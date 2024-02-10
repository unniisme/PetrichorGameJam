using System.Threading.Tasks;
using Gamelogic.Grid;
using Godot;

namespace Gamelogic.Objects
{
    public partial class BoxMonster : GridObject, IMorphable
    {
        private bool movable = true;
        private bool morphed = false;
        private bool attackInCooldown = false;
        [Export]
        public float cooldownTime = 1f;

        [Export]
        private GodotGridNavigationAgent agent;
        public override bool Movable => movable;

        public bool IsMorphed 
        {
            get => morphed;
            set
            {
                morphed = value;
                movable = !value;
            }
        }
        public void ToggleMorph() => IsMorphed = !IsMorphed;
        private bool CanSeePlayer()
        {
            Node2D result = grid.GridCast(GridPosition, grid.GetObjectPosition(GameManager.Player), agent.Depth);
            return result is Player;
        }

        public override void _Ready()
        {
            base._Ready();

            GameManager.RegisterMorphable(this);
        }

        public override void _Process(double delta)
        {
            base._Process(delta);

            if (!isMoving && IsMorphed && CanSeePlayer())
            {
                Vector2I nextPos = agent.GetNextPosition(GameManager.Grid.GetObjectPosition(GameManager.Player));
                Node2D nextObj = GameManager.Grid.GetObject(nextPos);
                if (nextObj is Player player && !attackInCooldown)
                    Attack(player);
                Move(nextPos - GridPosition);
            }
        }

        private async void Attack(Player player)
        {
            player.Hurt(this);
            attackInCooldown = true;
            await Task.Delay((int)cooldownTime*1000);
            attackInCooldown = false;
        }
    }
}