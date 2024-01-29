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

        private Vector2I memoryPosition;
        private bool useMemory = false;

        public bool IsMorphed 
        {
            get => morphed;
            set
            {
                morphed = value;
                movable = !value;
                useMemory = false;
            }
        }
        public void ToggleMorph() => IsMorphed = !IsMorphed;
        private bool CanSeePlayer()
        {
            Vector2I playerPosition = grid.GetObjectPosition(GameManager.Player);
            IGridObject result = grid.GridCast(GridPosition, playerPosition, agent.Depth);


            return result is Player || // Player is gridcastable, or player is within minSeeDistance
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

            if (!isMoving && IsMorphed)
            {
                if (CanSeePlayer())
                {
                    Vector2I playerPos = grid.GetObjectPosition(GameManager.Player);
                    Vector2I nextPos = agent.GetNextPosition(playerPos);
                    IGridObject nextObj = GameManager.Grid.GetObject(nextPos);
                    if (nextObj is Player player && !attackInCooldown)
                        Attack(player);
                    if(Move(nextPos - GridPosition))
                    {
                        memoryPosition = playerPos;
                        useMemory = true;
                    }
                }
                else if (useMemory)
                {
                    Vector2I nextPos = agent.GetNextPosition(memoryPosition);
                    IGridObject nextObj = GameManager.Grid.GetObject(nextPos);
                    if (nextObj is Player player && !attackInCooldown)
                        Attack(player);
                    Move(nextPos - GridPosition);
                    if (GridPosition == memoryPosition)
                        useMemory = false;
                }
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