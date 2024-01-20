using Gamelogic.Grid;
using Godot;

namespace Gamelogic
{
	public partial class GameManager : Node
	{
		private static IGrid grid = new MockGrid(new Vector2(32, 32));

		/// <summary>
		/// The grid for this game
		/// </summary>
		public static IGrid Grid => grid;
	}
}
