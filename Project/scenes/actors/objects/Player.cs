using Gamelogic.Grid;
using Godot;

namespace Gamelogic.Objects
{
	[GlobalClass]
	public partial class Player : CharacterBody2D, IMorphable, IGridObject
	{
		private IGrid grid;
		private bool morphed = false;
		private int health = 3;
		public bool inputEnabled = true;

		[Signal]
		public delegate void HealthChangedEventHandler(int health);

		public int Health
		{
			get => health;
			set
			{
				if (health <= 0) return;

				health = value;
				EmitSignal(SignalName.HealthChanged, health);
				if (health == 0)
				{
					GameManager.DelayedRestart();
					inputEnabled = false;
				}
			}
		}

		public bool IsMorphed
		{
			get => morphed;
			set => morphed = value;
		}
		public void ToggleMorph() => IsMorphed = !morphed;

		public Vector2I GridPosition => grid.GetObjectPosition(this);

		/// <summary>
		/// Movement speed, 1/gridcells per second
		/// </summary>
		public float speed = 4.8f;
		public float smoothness = 1f;

        public override void _Ready()
        {
            grid = GameManager.Grid;
			grid.PlaceObject(this);
			inputEnabled = true;

			GameManager.RegisterMorphable(this);
        }

        public override void _PhysicsProcess(double delta)
        {
			if (grid.GetObjectPosition(this) == grid.GameCoordinateToGridCoordinate(Position)
				&& inputEnabled)
			{
				Vector2 inputVector = Input.GetVector("left", "right", "up", "down");
				if (!inputVector.IsZeroApprox())
				Move(inputVector);
			}

			Velocity = Velocity.Lerp(
				(grid.GetObjectPositionInGameCoordinates(this) - Position)*speed,
				smoothness
			);
			MoveAndSlide();

			ZIndex = GameResources.baseLayerOffset + GridPosition.Y;
        }

		public bool Move(Vector2 dir)
		{
			if (dir.IsZeroApprox()) return false;

			Vector2I gridPosition = GridPosition;
			Vector2I targetPosition = grid.GetPositionInDirection(gridPosition, dir);
			
			if ((gridPosition - targetPosition).LengthSquared() > 1) return false; // Diagonal

			IGridObject obj = grid.GetObject(targetPosition);
			bool canMove = true;
			if (obj != null)
			{
				if (obj is GridObject gridObj && gridObj.Movable)
				{
					canMove = gridObj.Move(dir);
				}
			}

			if (canMove)
				return grid.MoveObjectInDirection(this, dir);
			else
				return false;
			
		}

		public bool Hurt(Node2D attacker)
		{
			Health -= 1;
			return Move(GlobalPosition - attacker.GlobalPosition);
		}
		

    }
}
