using System;
using Gamelogic.Grid;
using Godot;

namespace Gamelogic.Objects
{
	[GlobalClass]
	public partial class PressurePad : Area2D, IActivatable, IMorphable
	{
		private bool isActive;
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
					OnActivityChangedEvent?.Invoke(value);
				}
			}
		}

		public bool IsMorphed {get;set;}
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
				if (val) EmitSignal(SignalName.Activated);
				else EmitSignal(SignalName.Deactivated);
			};
		}

		private void GridChangeEventHandler(Vector2I pos)
		{
			if (pos == gridPosition && !IsMorphed)
			{
				IsActive = grid.GetObject(pos) != null;
			}
		}
	}
}
