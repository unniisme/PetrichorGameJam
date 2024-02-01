using System;
using Gamelogic.Audio;
using Gamelogic.Grid;
using Godot;

namespace Gamelogic.Objects
{
    public partial class Door : GridObject, IActivatable
    {
        private bool isActive = true;
        private CollisionShape2D collisionShape;

        [Export]
        public PressurePad pressurePad = null;

        public bool IsActive 
        {
            get => isActive;
            set
            {
                if (isActive != value)
                {
                    isActive = value;

                    if (value) 
                    {
                        IGridObject obj = grid.GetObject(GridPosition);
                        obj?.Kill(this);

                        grid.PlaceObject(this);
                        AudioManager.PlayStream("doorOpens");
                        ZIndex = GameResources.baseLayerOffset + 50;
                    }
                    else 
                    {
                        grid.RemoveObject(this);
                        AudioManager.PlayStream("doorCloses");
                        ZIndex = GameResources.baseLayerOffset - 50;
                    }

                    collisionShape.Disabled = !value;
                    OnActivityChangedEvent?.Invoke(value);

                }
            }
        }
        public event Action<bool> OnActivityChangedEvent;
        public override Vector2I GridPosition { get; set; }

        public override void _Ready()
        {
            base._Ready();

            GridPosition = grid.GetObjectPosition(this);
            OnActivityChangedEvent?.Invoke(true);
            collisionShape = GetNode<CollisionShape2D>("CollisionShape2D");
            if (snap) Position = grid.GridCoordinateToGameCoordinate(GridPosition);

            if (pressurePad != null)
            {
                pressurePad.Activated += Open;
                pressurePad.Deactivated += Close;
            }
        }

        public override void _PhysicsProcess(double delta)
        {
            // override
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
