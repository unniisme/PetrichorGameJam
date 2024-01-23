using System;
using Godot;

namespace Gamelogic.Objects
{
    public partial class Door : StaticBody2D, IActivatable
    {
        private bool isActive = true;
        private CollisionShape2D collisionShape;

        public bool IsActive 
        {
            get => isActive;
            set
            {
                isActive = value;
                collisionShape.Disabled = !value;
                OnActivityChangedEvent?.Invoke(value);
            }
        }
        public event Action<bool> OnActivityChangedEvent;

        public override void _Ready()
        {
            collisionShape = GetNode<CollisionShape2D>("CollisionShape2D");
            IsActive = true;
        }

        public void Open() => IsActive = false;
        public void Close() => IsActive = true;
    }
}
