using Gamelogic.Grid;
using Godot;

namespace Gamelogic.Objects
{
	public partial class Player : GridObject
	{
		public override void _Process(double delta)
		{
			base._Process(delta);

			if (!isMoving)
			{
				Vector2 inp = Input.GetVector("left", "right", "up", "down");
				Move(inp);
			}
		}
	}
}
