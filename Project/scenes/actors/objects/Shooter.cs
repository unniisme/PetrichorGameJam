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
        private uint charge = 0;
        private int curr = 0;
        private int Curr
        {
            get => curr;
            set => curr = value%3;
        }
        public bool IsCharged => charge > 0;
        public FacingDirection Facing
        {
            get => Curr switch
            {
                0 => dir0,
                1 => dir1,
                2 => dir2,
                _ => FacingDirection.left
            };
        }

        // For animation reference
        public FacingDirection dir0;
        public FacingDirection dir1;
        public FacingDirection dir2;


        [Export(PropertyHint.Enum, "left,right,up,down")]
        public string Dir0
        {
            get => Enum.GetName(dir0);
            set => _ = Enum.TryParse(value, out dir0);
        }
        [Export(PropertyHint.Enum, "left,right,up,down")]
        public string Dir1
        {
            get => Enum.GetName(dir1);
            set => _ = Enum.TryParse(value, out dir1);
        }
        [Export(PropertyHint.Enum, "left,right,up,down")]
        public string Dir2
        {
            get => Enum.GetName(dir2);
            set => _ = Enum.TryParse(value, out dir2);
        }

        [Export]
        public Shooter dir0Shooter = null;
        [Export]
        public Shooter dir1Shooter = null;
        [Export]
        public Shooter dir2Shooter = null;
        [Export]
        public TileMap dir0Glyph = null;
        [Export]
        public TileMap dir1Glyph = null;
        [Export]
        public TileMap dir2Glyph = null;

        [Signal]
        public delegate void Fire0EventHandler(bool chargeUp);
        [Signal]
        public delegate void Fire1EventHandler(bool chargeUp);
        [Signal]
        public delegate void Fire2EventHandler(bool chargeUp);

        public event Action<FacingDirection> DirectionChanged;

        public override void _Ready()
        {
            base._Ready();

            if (dir0Shooter != null)
            {
                Fire0 += dir0Shooter.HandleChargeChange;
            }
            if (dir1Shooter != null)
            {
                Fire1 += dir1Shooter.HandleChargeChange;
            }
            if (dir2Shooter != null)
            {
                Fire2 += dir2Shooter.HandleChargeChange;
            }

            if (dir0Glyph != null)
            {
                Connect(SignalName.Fire0, (Callable)dir0Glyph.Get("change_light"));
            }
            if (dir1Glyph != null)
            {
                Connect(SignalName.Fire1, (Callable)dir1Glyph.Get("change_light"));
            }
            if (dir2Glyph != null)
            {
                Connect(SignalName.Fire2, (Callable)dir2Glyph.Get("change_light"));
            }
        }

        /// <summary>
        /// Call when charging up
        /// </summary>
        public void ChargeUp()
        {
            charge += 1;
            if (charge == 1) FireCurr(true);
        }
        /// <summary>
        /// Call when charging down (duh)
        /// </summary>
        public void ChargeDown()
        {
            charge -= 1;
            if (charge == 0) FireCurr(false);
        }

        public void HandleChargeChange(bool chargeUp)
        {
            if (chargeUp) ChargeUp();
            else ChargeDown();
        }

        public void Rotate()
        {
            FireCurr(false);
            Curr += 1;
            if (IsCharged) FireCurr(true);
            DirectionChanged?.Invoke(Facing);
        }

        private void FireCurr(bool chargeUp)
        {
            EmitSignal($"Fire{Curr}", chargeUp);
        }
    }
}
