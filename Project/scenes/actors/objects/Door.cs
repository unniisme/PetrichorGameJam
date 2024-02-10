using System;
using Gamelogic.Grid;
using Godot;

namespace Gamelogic.Objects
{
    public partial class Door : GridObject, IActivatable
    {
        private bool isActive = true;
        private CollisionShape2D collisionShape;

        public bool IsActive 
        {
            get => isActive;
            set
            {
                isActive = value;

                if (value) grid.PlaceObject(this);
                else grid.RemoveObject(this);

                collisionShape.Disabled = !value;
                OnActivityChangedEvent?.Invoke(value);
            }
        }
        public event Action<bool> OnActivityChangedEvent;

        public override void _Ready()
        {
            base._Ready();
            OnActivityChangedEvent?.Invoke(true);
            collisionShape = GetNode<CollisionShape2D>("CollisionShape2D");
        }

        public void Open() => IsActive = false;
        public void Close() => IsActive = true;
        public void SetOpen(bool open)
        {
            if(open) Open(); 
            else Close();
        }
    }
}
