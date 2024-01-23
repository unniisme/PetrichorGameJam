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
		internal bool isMoving = false;
		internal float moveTime = 0.2f; // Time to animate movement in seconds
		internal float movementFraction = 0f; 
		internal Vector2 initialVector;
		internal Vector2 finalVector;

		public IGrid grid;

		[Export]
		public bool snap = true;
		public Vector2I GridPosition
		{
			get => grid.GetObjectPosition(this);
			set => grid.MoveObject(this, value);
		}

		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			grid = GameManager.Grid;

			// Register on the grid
			grid.PlaceObject(this, grid.GameCoordinateToGridCoordinate(Position));
		}

		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _PhysicsProcess(double delta)
		{
			// Snap to grid
			if (snap && !isMoving)
			{
				Position = grid.GridCoordinateToGameCoordinate(GridPosition);
			}
			else if (isMoving)
			{
				ProcessMove((float)delta);
			}
		}

		public bool Move(Vector2 dir)
		{
			if (isMoving) return false;
			if (grid.MoveObjectInDirection(this, dir))
			{
				SetMoving();
				return true;
			}
			return false;
		}

		internal void SetMoving()
		{
			isMoving = true;
			initialVector = Position;
			finalVector = grid.GridCoordinateToGameCoordinate(GridPosition);
			movementFraction = 0;
		}
		internal void UnsetMoving()
		{
			isMoving = false;
		}
		internal virtual void ProcessMove(float delta)
		{
			Position = initialVector.Lerp(finalVector, movementFraction);
			movementFraction += delta/moveTime;
			if (movementFraction >= 1)
				UnsetMoving();
		}
	}
}
