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
        private int minSeeDistance = 4; // Minimum manhatten distance at which isSee is trivially true

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
            Vector2I playerPosition = grid.GetObjectPosition(GameManager.Player);
            Node2D result = grid.GridCast(GridPosition, playerPosition, agent.Depth);


            return 
                result is Player || // Player is gridcastable, or player is within minSeeDistance
                Mathf.Abs(playerPosition.X - GridPosition.X) + Mathf.Abs(playerPosition.Y - GridPosition.Y) < minSeeDistance;
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