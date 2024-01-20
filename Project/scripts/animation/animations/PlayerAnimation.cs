using System;
using Gamelogic.Objects;
using Godot;

namespace AnimationHandling
{
    public partial class PlayerAnimation : AnimationHandler
    {
        private Player player;

        [Export]
        public float movementSpeedCutoff = 7;

        AnimationState idle;
        AnimationState moving;
        public override void _Ready()
        {
            base._Ready();

            idle = AddState("Idle", "idle");
            moving = AddState("Moving", "moving");

            player = GetParent<Player>();
        }

        public override void _Process(double delta)
        {
            HorizontalFlip = Math.Sign(player.Velocity.X) < 0;

            if (player.Velocity.LengthSquared() < movementSpeedCutoff)
                Transition(idle);
            else
                Transition(moving);
        }
    }
}