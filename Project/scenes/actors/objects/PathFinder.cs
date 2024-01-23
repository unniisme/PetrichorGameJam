using System;
using Gamelogic.Grid;
using Godot;

namespace Gamelogic.Objects
{
	public partial class PathFinder : GridObject
	{
		private IGridNavigationAgent navigationAgent;
		private Vector2I[] path = Array.Empty<Vector2I>();

		[Export]
		public int range = 100;

        public override void _Ready()
        {
			base._Ready();

            navigationAgent = new AStarNavigationAgent(grid, this, range);
        }

        public override void _Process(double delta)
        {
			base._Process(delta);

			if (isMoving) return;

            Vector2I targetPos = grid.GameCoordinateToGridCoordinate(GetGlobalMousePosition());	
			Vector2I nextStep = navigationAgent.GetNextPosition(targetPos);
			Move(nextStep - grid.GetObjectPosition(this));
        }

        public override void _Draw()
        {
			for (int i=0; i<path.Length-1; i++)
			{
				Vector2 from = ToLocal(grid.GridCoordinateToGameCoordinate(path[i]));
				Vector2 to = ToLocal(grid.GridCoordinateToGameCoordinate(path[i+1]));
            	DrawLine(from, to, Colors.Red, 1);
			}
        }
    }
}
