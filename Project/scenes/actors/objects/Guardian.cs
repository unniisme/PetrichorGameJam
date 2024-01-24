using System;
using Godot;

namespace Gamelogic.Objects
{
    [GlobalClass]
    public partial class Guardian : Node2D, IActivatable, IMorphable
    {
        private bool isActive = false; 
        public bool IsActive { 
            get => isActive; 
            set
            {
                isActive = value;
                OnActivityChangedEvent?.Invoke(value);
            } 
        }

        public bool IsMorphed { get; set; }
        public void ToggleMorph() => IsMorphed = !IsMorphed;

        public event Action<bool> OnActivityChangedEvent;


        public override void _Ready()
        {
            GameManager.RegisterMorphable(this);
        }
        public override void _PhysicsProcess(double delta)
        {
            if (IsMorphed)
            {
                if ((GameManager.Player.GlobalPosition - GlobalPosition).LengthSquared() < 5000)
                {
                    IsActive = true;
                    if ((GameManager.Player.GlobalPosition - GlobalPosition).LengthSquared() < 1200)
                    {
                        GameManager.Player.Hurt(this);
                    }
                }
                else
                    IsActive = false;
            }
        }
    }
}