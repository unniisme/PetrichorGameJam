using Godot;

namespace Gamelogic.Grid
{
	[GlobalClass]
	/// <summary>
	/// A godot object that is suppose to reside in a grid
	/// </summary>
	public partial class GridObject : Node2D
	{

		// Lerping
		private bool isMoving = false;
		private float moveTime = 0.2f; // Time to animate movement in seconds
		float movementFraction = 0f; 
		Vector2 initialVector;
		Vector2 finalVector;

		public IGrid grid;

		[Export]
		public bool snap;
		public Vector2I GridPosition
		{
			get => grid.GetObjectPosition(this);
			set => grid.MoveObject(this, value);
		}

		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			grid = GetNode<GameGrid>("/root/GameGrid");

			// Register on the grid
			grid.PlaceObject(this, grid.GameCoordinateToGridCoordinate(Position));
		}

		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _Process(double delta)
		{
			// Snap to grid
			if (snap && !isMoving)
			{
				Position = grid.GridCoordinateToGameCoordinate(GridPosition);
			}
			else if (isMoving)
			{
				Position = initialVector.Lerp(finalVector, movementFraction);
				movementFraction += ((float)delta)/moveTime;
				if (movementFraction >= 1)
					UnsetMoving();
			}
		}

		public void Move(Vector2 dir)
		{
			grid.MoveObjectInDirection(this, dir);
			SetMoving();
		}

		private void SetMoving()
		{
			isMoving = true;
			initialVector = Position;
			finalVector = grid.GridCoordinateToGameCoordinate(GridPosition);
			movementFraction = 0;
		}
		private void UnsetMoving()
		{
			isMoving = false;
		}
	}
}
