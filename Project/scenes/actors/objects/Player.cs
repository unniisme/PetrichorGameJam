using Gamelogic.Grid;
using Godot;

namespace Gamelogic.Objects
{
	[GlobalClass]
	public partial class Player : CharacterBody2D
	{
		private IGrid grid;

		/// <summary>
		/// Movement speed, 1/gridcells per second
		/// </summary>
		public float speed = 7;
		public float smoothness = 1f;

        public override void _Ready()
        {
            grid = GameManager.Grid;
			grid.PlaceObject(this);
        }

        public override void _PhysicsProcess(double delta)
        {
			if (grid.GetObjectPosition(this) == grid.GameCoordinateToGridCoordinate(Position))
			{
				Vector2 inputVector = Input.GetVector("left", "right", "up", "down");
				grid.MoveObjectInDirection(this, inputVector);
			}

			Velocity = Velocity.Lerp(
				(grid.GetObjectPositionInGameCoordinates(this) - Position)*speed,
				smoothness
			);
			MoveAndSlide();
        }
    }
}
