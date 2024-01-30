using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Gamelogic.Grid;
using Gamelogic.Objects;
using Godot;

namespace Gamelogic
{
	public partial class GameManager : Node
	{
		private static IGrid grid = new GodotGrid(new (32, 32), new (16,16));
		private static List<IMorphable> morphables = new();
		private static GameManager runningManager;
		private static LevelManager level;
		public static int leftoverCrystals = 0;

		public GameManager()
		{
			runningManager = this;
		}

		private static void ResetGrid() => grid = new GodotGrid(new (32, 32), new (16,16));
		private static void ResetMorphables()
		{
			IsMorphed = false;
			morphables = new();
		}
		private static void Reset()
		{
			ResetGrid();
			ResetMorphables();
		}

		/// <summary>
		/// The grid for this game
		/// </summary>
		public static IGrid Grid => grid;

		/// <summary>
		/// The running player
		/// </summary>
		public static Player Player => level.Player;
		public static Player GetPlayer() => Player;

		/// <summary>
		/// The current running level
		/// </summary>
		/// <returns></returns>
		public static LevelManager GetLevel() => level;

		public const uint UnMorphedBitmask = 2; // Layer 2
		public const uint MorphedBitmask = 4; // Layer 3

		/// <summary>
		/// Register an object as a morphable
		/// </summary>
		/// <param name="obj"></param>
		public static void RegisterMorphable(IMorphable obj) => morphables.Add(obj);

		/// <summary>
		/// Register this level
		/// Should be replaced later in favour of controlling level loading
		/// </summary>
		/// <param name="pl"></param>
		public static void RegisterLevel(LevelManager levelManager) => level = levelManager;

		public static void RegisterGrid(IGrid godotGrid) => grid = godotGrid;
		
		private static bool isMorphed = false;
		public static bool IsMorphed
		{
			get => isMorphed;
			set
			{
				foreach (IMorphable morp in morphables)
				{
					morp.IsMorphed = value;
				}
				runningManager.GetViewport().CanvasCullMask = value?
												MorphedBitmask+1:UnMorphedBitmask+1; // +1 for layer 1
				isMorphed = value;
			}
		}
		/// <summary>
		/// Morph!!!
		/// </summary>
		public static void ToggleMorph() => IsMorphed = !IsMorphed;

		/// <summary>
		/// Call on player death
		/// </summary>
		public static async void DelayedRestart()
		{
			await Task.Delay(1000);
			Restart();
		}
		
		/// <summary>
		/// Restart the current level
		/// </summary>
		public static void Restart()
		{
			Reset();
			runningManager.GetTree().ReloadCurrentScene();
			runningManager.GetTree().Paused = false;
		}

		/// <summary>
		/// Replace current scene with the next level in list of levels found in GameResources
		/// </summary>
		public static void LoadNextLevel()
		{
			Reset();
			leftoverCrystals = level.MorphCharges;
			int index = (level==null)?0:level.levelId+1;
			runningManager.GetTree().ChangeSceneToFile(GameResources.Levels[index]);
			runningManager.GetTree().Paused = false;
		}

		/// <summary>
		/// Replace current scene with the main menu of the game
		/// </summary>
		public static void LoadMainMenu()
		{
			Reset();

			runningManager.GetTree().ChangeSceneToFile(GameResources.mainMenuScene);
			runningManager.GetTree().Paused = false;
			level = null;
		}



        public override void _Ready()
        {
            GetViewport().CanvasCullMask = UnMorphedBitmask+1;
			ProcessMode = ProcessModeEnum.Always;
        }

        public override void _Process(double delta)
        {
			if (Input.IsActionJustPressed("menu"))
            {
                GetLevel().EmitSignal(LevelManager.SignalName.Menu);
            }
        }
    }
}
