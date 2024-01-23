using System.Collections.Generic;
using System.Threading;
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

		/// <summary>
		/// Wrapper for placing an object on grid
		/// </summary>
		public static void PlaceObjectOnGrid(Node2D obj) => grid.PlaceObject(obj);
		
		public static void RemoveObjectFromGrid(Node2D obj) => grid.RemoveObject(obj);

		public const uint UnMorphedBitmask = 1;
		public const uint MorphedBitmask = 2;

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

		public static void EndGame() => runningManager.Restart();

		/// <summary>
		/// Call on player death
		/// </summary>
		public void Restart()
		{
			GetTree().Paused = true;
			Thread.Sleep(1000);
			GetTree().ReloadCurrentScene();
			ResetGrid();
			ResetMorphables();
			GetTree().Paused = false;

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
