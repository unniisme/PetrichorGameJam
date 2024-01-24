using Gamelogic.Grid;
using Godot;

namespace Gamelogic.Objects
{
    public partial class BoxMonster : GridObject, IMorphable
    {
        private bool movable = true;
        private bool morphed = false;

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
                Vector2I nextPos = agent.GetNextPosition(GameManager.Grid.GetObjectPosition(GameManager.Player));
                Node2D nextObj = GameManager.Grid.GetObject(nextPos);
                if (nextObj is Player player)
                    player.Hurt(this);
                Move(nextPos - GridPosition);
            }
        }
    }
}