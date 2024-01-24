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
		private static Player player;
		private static GameManager runningManager;

		public GameManager()
		{
			runningManager = this;
		}

		public static void ResetGrid() => grid = new GodotGrid(new (32, 32), new (16,16));
		public static void ResetMorphables()
		{
			IsMorphed = false;
			morphables = new();
		}

		/// <summary>
		/// The grid for this game
		/// </summary>
		public static IGrid Grid => grid;

		/// <summary>
		/// The running player
		/// </summary>
		public static Player Player => player;

		public const uint UnMorphedBitmask = 2;
		public const uint MorphedBitmask = 1;

		/// <summary>
		/// Register an object as a morphable
		/// </summary>
		/// <param name="obj"></param>
		public static void RegisterMorphable(IMorphable obj) => morphables.Add(obj);

		/// <summary>
		/// Register this object as the player for the game/scene
		/// </summary>
		/// <param name="pl"></param>
		public static void RegisterPlayer(Player pl) => player = pl; 
		
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
												MorphedBitmask:UnMorphedBitmask;
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
		public static async void EndGame()
		{
			player.inputEnabled = false;
			await Task.Delay(1000);
			runningManager.GetTree().ReloadCurrentScene();
			ResetGrid();
			ResetMorphables();
			player.inputEnabled = true;
		}

        public override void _Process(double delta)
        {
            if (Input.IsActionJustPressed("action"))
			{
				ToggleMorph();
			}
        }

        public override void _Ready()
        {
            GetViewport().CanvasCullMask = UnMorphedBitmask;
        }
    }
}
