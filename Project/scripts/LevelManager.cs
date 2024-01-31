using Gamelogic.Audio;
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
		private int morphCharges = 6;
		[Export]
		private int maxMorphCharges = 6;

		[Signal]
		public delegate void MorphChargesChangedEventHandler(int val);

		[Signal]
		public delegate void MenuEventHandler();


		public int MorphCharges
		{
			get => morphCharges;
			set
			{
				if (value < 0 || value > maxMorphCharges) return;
				
				morphCharges = value;
				EmitSignal(SignalName.MorphChargesChanged, value);
			}
		}

		public int MaxMorphCharges => maxMorphCharges; // Define setter if required

		public Player Player => player;
		public Player GetPlayer() => player;
		public CanvasLayer HUD => hud;
		public CanvasLayer GetHUD() => hud;
		public Camera2D Camera => cam;
		public Camera2D GetCamera() => cam;

		/// <summary>
		/// Try to increase the value of morph charges by 1 crystal.
		/// </summary>
		/// <returns>Whether the operation was successfull</returns>
		public bool IncrementMorphCharges()
		{
			int initialMorphCharges = MorphCharges;
			MorphCharges += 2;
			return initialMorphCharges < MorphCharges;
		}

		public override void _Ready()
		{
			GameManager.RegisterLevel(this);

			player ??= GetNode<Player>("Player");
			hud ??= GetNode<CanvasLayer>("HUD");
			cam ??= GetNode<Camera2D>("Camera");

			MorphCharges += GameManager.leftoverCrystals;
			hud.Call("initialize");
		}

		public override void _Process(double delta)
		{
			if (MorphCharges > 0 && Input.IsActionJustPressed("action"))
			{
				MorphCharges -= 1;

				if (MorphCharges%2 == 0)
				{
					AudioManager.PlayStream("reusedCrystal");
				}
				else
				{
					AudioManager.PlayStream("usedCrystal");
				}

				GameManager.ToggleMorph();
			}
		}
	}
}
