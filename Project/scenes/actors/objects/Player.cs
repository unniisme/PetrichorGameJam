using System.Threading.Tasks;
using Gamelogic.Grid;
using Godot;

namespace Gamelogic.Objects
{
	[GlobalClass]
	public partial class Player : CharacterBody2D, IMorphable
	{
		private IGrid grid;
		private bool morphed = false;
		private int health = 3;
		public bool inputEnabled = true;

		public int Health
		{
			get => health;
			set
			{
				health = value;
				if (health == 0)
				{
					GameManager.EndGame();
				}
			}
		}

		public bool IsMorphed
		{
			get => morphed;
			set => morphed = value;
		}
		public void ToggleMorph() => IsMorphed = !morphed;

		/// <summary>
		/// Movement speed, 1/gridcells per second
		/// </summary>
		public float speed = 7;
		public float smoothness = 1f;

        public override void _Ready()
        {
            grid = GameManager.Grid;
			grid.PlaceObject(this);

			GameManager.RegisterMorphable(this);
			GameManager.RegisterPlayer(this);
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
        }

		private bool Move(Vector2 dir)
		{
			Node2D obj = grid.GetObject(grid.GetPositionInDirection(grid.GetObjectPosition(this), dir));
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

		public void Hurt(Node2D attacker)
		{
			Health -= 1;
			Move(GlobalPosition - attacker.GlobalPosition);
		}
		

    }
}
