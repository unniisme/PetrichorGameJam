using Gamelogic.Grid;
using Godot;

namespace AnimationHandling
{
    public partial class GridObjectAnimation : AnimationHandler
    {
        private Node2D node;
        private GridObject gridObject;

        AnimationState idle;
        AnimationState moving;

        public override void _Ready()
        {
            base._Ready();

            idle = AddState("Idle", "idle");
            moving = AddState("Moving", "moving");

            node = GetParent<Node2D>();

            if (node is not GridObject || node == null)
            {
                GD.PrintErr("Grid Object animation connected to non grid object");
            }

            gridObject = (GridObject) node;
        }

        public override void _Process(double delta)
        {
            base._Process(delta);

            if (gridObject.isMoving)
                Transition(moving);
            else
                Transition(idle);

            HorizontalFlip = (node.Position - gridObject.grid.GetObjectPositionInGameCoordinates((IGridObject)node)).X > 0;
        }
    }
}
