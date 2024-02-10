using Gamelogic;
using Gamelogic.Grid;
using Godot;
using System;

namespace Gamelogic.Objects
{
    public partial class Fence : AnimatableBody2D, IMorphable, IGridObject
    {
        private bool isMorphed;
        private CollisionShape2D collisionShape;
        private IGrid grid;

        public bool IsMorphed 
        { 
            get => isMorphed; 
            set
            {
                if (isMorphed != value)
                {
                    if (value)
                    {
                        grid.RemoveObject(this);
                        collisionShape.Disabled = true;
                    } 
                    else
                    {
                        // Player too close
                        if ((GameManager.Player.GlobalPosition - GlobalPosition).LengthSquared() < 20*20)
                        {
                            GameManager.Player.Hurt(this);
                        }

                        grid.GetObject(GridPosition)?.Kill(this);
                        grid.PlaceObject(this);
                        collisionShape.Disabled = false;
                    }
                }
                isMorphed = value;
            } 
        }

        public void ToggleMorph() => IsMorphed = !IsMorphed;

        public Vector2I GridPosition {get; set;}

        public override void _Ready()
        {
            GameManager.RegisterMorphable(this);

            collisionShape = GetNode<CollisionShape2D>("CollisionShape2D");
            grid = GameManager.Grid;

            grid.PlaceObject(this);
            collisionShape.Disabled = false;

            GridPosition = grid.GetObjectPosition(this);
        }

        public bool Move(Vector2 _) => false; // Can't move this
        public bool Kill(Node2D _) => false; // Can't hurt this either

    }
}
