using Gamelogic.Grid;
using Godot;
using System;

namespace Gamelogic.Objects
{
	public partial class PressurePad : Area2D
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
					if (isActive)
						EmitSignal(SignalName.Activated);
					else
						EmitSignal(SignalName.Deactivated);
				}
			}
		}

		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			grid = GameManager.Grid;
			grid.GridChangeEvent += GridChangeEventHandler;
			gridPosition = grid.GameCoordinateToGridCoordinate(GlobalPosition);
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
