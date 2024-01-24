using Gamelogic.Grid;
using Godot;
using System;

namespace Gamelogic.Objects
{
    public enum FacingDirection
    {
        left,
        right,
        up,
        down
    }

    public partial class Shooter : GridObject
    {
        private FacingDirection facing;
        private bool charged = false;

        [Export]
        public Shooter leftTarget;
        [Export]
        public Shooter rightTarget;
        [Export]
        public Shooter upTarget;
        [Export]
        public Shooter downTarget;

        [Export(PropertyHint.Enum, "left,right,up,down")]
        public string FacingString
        {
            get => Enum.GetName(facing);
            set
            {
                _ = Enum.TryParse(value, out facing);
            }
        }

        public FacingDirection Facing
        {
            get => facing;
            set
            {
                if (facing != value)
                {

                }
            }
        }

        public bool Charged
        {
            get => charged;
            set
            {
                charged = value;
                ShooterInDirection(facing).Charged = value;
            }
        }

        public void ChargeUp()
        {
            charged = true;
        }

        public void ChargeDown()
        {
            charged = false;
        }

        public Shooter ShooterInDirection(FacingDirection dir) => dir switch
            {
                FacingDirection.left => leftTarget,
                FacingDirection.right => rightTarget,
                FacingDirection.down => downTarget,
                FacingDirection.up => upTarget,
                _ => null,
            };
    }
}
