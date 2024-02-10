using Gamelogic.Objects;
using Godot;

namespace AnimationHandling
{
    [GlobalClass]
    public partial class ShooterAnimation : AnimationHandler
    {
        private Shooter shooter;
        AnimationState left;
        AnimationState right;
        AnimationState up;
        AnimationState down;

        public override void _Ready()
        {
            base._Ready();

            shooter = GetParent<Shooter>();
            shooter.DirectionChanged += ChangeDirection;

            left = AddState("left", "left");
            right = AddState("right", "right");
            up = AddState("up", "up");
            down = AddState("down", "down");
            ChangeDirection(shooter.Facing);
            Play();
        }

        private void ChangeDirection(FacingDirection direction)
        {
            switch (direction)
            {
                case FacingDirection.left:
                    Transition(left);
                    break;
                case FacingDirection.right:
                    Transition(right);
                    break;
                case FacingDirection.up:
                    Transition(up);
                    break;
                case FacingDirection.down:
                    Transition(down);
                    break;
            }
        }
    }
}