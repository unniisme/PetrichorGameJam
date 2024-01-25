using Gamelogic.Objects;
using Godot;

namespace Gamelogic
{
    [GlobalClass]
    public partial class LevelManager : Node2D
    {
        private Player player;
        private CanvasLayer hud;
        private int morphCharges = 6;

        [Signal]
        public delegate void MorphChargesChangedEventHandler(int val);

        [Signal]
        public delegate void MenuEventHandler();

        public int MorphCharges
        {
            get => morphCharges;
            set
            {
                if (morphCharges <= 0) return;
                morphCharges = value;
                EmitSignal(SignalName.MorphChargesChanged, value);
            }
        }

        public Player Player => player;
        public CanvasLayer HUD => hud;

        public override void _Ready()
        {
            GameManager.RegisterLevel(this);

            player = GetNode<Player>("Player");
            hud = GetNode<CanvasLayer>("HUD");

            hud.Call("connect_signals");
        }

        public override void _Process(double delta)
        {
            if (MorphCharges > 0 && Input.IsActionJustPressed("action"))
			{
                MorphCharges -= 1;
				GameManager.ToggleMorph();
			}
        }
    }
}