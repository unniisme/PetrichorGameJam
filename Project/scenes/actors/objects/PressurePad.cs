using System;
using Gamelogic.Audio;
using Gamelogic.Grid;
using Godot;

namespace Gamelogic.Objects
{

	/// <summary>
	/// Only emmits when unmorphed
	/// </summary>
	[GlobalClass]
	public partial class PressurePad : Area2D, IActivatable, IMorphable
	{
		private bool isActive;
		private bool isMorphed = false;
		private Vector2I gridPosition;
		private IGrid grid;

		[Signal]
		public delegate void ActivatedEventHandler();
		[Signal]
		public delegate void DeactivatedEventHandler();

		public bool IsActive
		{
			get => isActive;
			set
			{
				if (value != isActive)
				{
					isActive = value;
					if (!isMorphed) OnActivityChangedEvent?.Invoke(value);
				}
			}
		}

		public bool IsMorphed
		{
			get => isMorphed;
			set
			{
				isMorphed = value;
				if (!value) 
					OnActivityChangedEvent?.Invoke(isActive);
			}
		}
		public void ToggleMorph() => IsMorphed = !IsMorphed;

		public event Action<bool> OnActivityChangedEvent;

		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			grid = GameManager.Grid;
			grid.GridChangeEvent += GridChangeEventHandler;
			gridPosition = grid.GameCoordinateToGridCoordinate(GlobalPosition);
			GameManager.RegisterMorphable(this);

			OnActivityChangedEvent += (bool val) =>
			{
				if (val)
				{
					AudioManager.PlayStream("pressurePlate");
					EmitSignal(SignalName.Activated);
				}
				else 
				{
					EmitSignal(SignalName.Deactivated);
				}
			};
		}

		private void GridChangeEventHandler(Vector2I pos)
		{
			if (pos == gridPosition)
			{
				IsActive = grid.GetObject(pos) != null;
			}
		}
	}
}
