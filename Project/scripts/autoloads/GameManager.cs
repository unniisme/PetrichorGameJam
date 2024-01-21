using System.Collections.Generic;
using Gamelogic.Grid;
using Godot;

namespace Gamelogic
{
	public partial class GameManager : Node
	{
		private readonly static IGrid grid = new GodotGrid(new (32, 32), new (16,16));
		private readonly static List<IMorphable> morphables = new();

		/// <summary>
		/// The grid for this game
		/// </summary>
		public static IGrid Grid => grid;

		/// <summary>
		/// Wrapper for placing an object on grid
		/// </summary>
		public static void PlaceObjectOnGrid(Node2D obj) => grid.PlaceObject(obj);
		
		public static void RemoveObjectFromGrid(Node2D obj) => grid.RemoveObject(obj);

		/// <summary>
		/// Register an object as a morphable
		/// </summary>
		/// <param name="obj"></param>
		public static void RegisterMorphable(IMorphable obj) => morphables.Add(obj);
		
		/// <summary>
		/// Morph!!!
		/// </summary>
		public static void ToggleMorph()
		{
			foreach (IMorphable morp in morphables)
			{
				morp.ToggleMorph();
			}
		}

        public override void _Process(double delta)
        {
            if (Input.IsActionJustPressed("action"))
				ToggleMorph();
        }
    }
}
