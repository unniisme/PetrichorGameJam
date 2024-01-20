using Gamelogic.Grid;
using Godot;

namespace Gamelogic
{
	public partial class GameManager : Node
	{
		private readonly static IGrid grid = new GodotGrid(new (32, 32), new (16,16));

		/// <summary>
		/// The grid for this game
		/// </summary>
		public static IGrid Grid => grid;
	}
}
