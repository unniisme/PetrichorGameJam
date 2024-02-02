using System;
using System.ComponentModel;
using Gamelogic.Audio;
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
        AnimationState death;
        public override void _Ready()
        {
            base._Ready();

            idle = AddState("Idle", "idle");
            moving = AddState("Moving", "moving");
            death = AddState("Death", "death");

            player = GetParent<Player>();
        }

        public override void _Process(double delta)
        {
            HorizontalFlip = Math.Sign(player.Velocity.X) < 0;

            if (player.Health <= 0)
            {
                AudioManager.StopStream("footsteps");
                Transition(death);
                return;
            }

            if (player.Velocity.LengthSquared() < movementSpeedCutoff)
            {
                AudioManager.StopStream("footsteps");
                Transition(idle);
            }
            else
            {
                if(!AudioManager.IsPlaying("footsteps"))
                {
                    AudioManager.PlayStream("footsteps");
                }
                Transition(moving);
            }
        }
    }
}