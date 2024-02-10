using Gamelogic;
using Gamelogic.Grid;
using Godot;
using System;

namespace Gamelogic.Objects
{
    public partial class Fence : AnimatableBody2D, IMorphable
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
                        grid.PlaceObject(this);
                        collisionShape.Disabled = false;
                    }
                }
                isMorphed = value;
            } 
        }

        public void ToggleMorph() => IsMorphed = !IsMorphed;

        public override void _Ready()
        {
            GameManager.RegisterMorphable(this);

            collisionShape = GetNode<CollisionShape2D>("CollisionShape2D");
            grid = GameManager.Grid;

            grid.PlaceObject(this);
            collisionShape.Disabled = false;
        }
    }
}
