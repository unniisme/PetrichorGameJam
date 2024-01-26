using Gamelogic.Objects;
using Godot;

namespace Gamelogic
{
    [GlobalClass]
    public partial class LevelManager : Node2D
    {
        [Export]
        public int levelId;

        [Export]
        private Player player;
        [Export]
        private CanvasLayer hud;
        [Export]
        private Camera2D cam;
        [Export]
        public int morphCharges = 6;

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
        public Player GetPlayer() => player;
        public CanvasLayer HUD => hud;
        public CanvasLayer GetHUD() => hud;
        public Camera2D Camera => cam;
        public Camera2D GetCamera() => cam;


        public override void _Ready()
        {
            GameManager.RegisterLevel(this);

            player ??= GetNode<Player>("Player");
            hud ??= GetNode<CanvasLayer>("HUD");
            cam ??= GetNode<Camera2D>("Camera");

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