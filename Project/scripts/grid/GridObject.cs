using Godot;

namespace Gamelogic.Grid
{
	/// <summary>
	/// A godot object that is suppose to reside in a grid
	/// </summary>
	public partial class GridObject : Node2D
	{
		public IGrid grid;
		public Vector2I GridPosition {get; set;}

		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			// Register on the grid
			GridPosition = grid.GameCoordinateToGridCoordinate(Position);
			grid.PlaceObject(this, GridPosition);
		}

		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _Process(double delta)
		{
			// Snap to grid
			Position = grid.GridCoordinateToGameCoordinate(GridPosition);

			// Get movement from input and move in the corresponding direction on the grid
			Vector2 inputDirection = Input.GetVector("left", "right", "up", "down");
			grid.MoveObjectInDirection(this, inputDirection);
		}
	}
}
