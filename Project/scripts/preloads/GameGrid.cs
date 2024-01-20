using Godot;

namespace Gamelogic.Grid
{
	public partial class GameGrid : Node, IGrid
	{
		public IGrid grid;

        public Vector2 Scale { get => grid.Scale;  set => grid.Scale = value; }
        public Vector2 Offset { get => grid.Offset;  set => grid.Offset = value; }

        public Vector2I GameCoordinateToGridCoordinate(Vector2 pos)
        {
            return grid.GameCoordinateToGridCoordinate(pos);
        }

        public Vector2 GridCoordinateToGameCoordinate(Vector2I pos)
        {
            return grid.GridCoordinateToGameCoordinate(pos);
        }

        public bool PlaceObject(Node2D obj, Vector2I pos)
        {
            return grid.PlaceObject(obj, pos);
        }

        public Node2D GetObject(Vector2I pos)
        {
            return grid.GetObject(pos);
        }

        public bool RemoveObject(Vector2I pos)
        {
            return grid.RemoveObject(pos);
        }

        public bool RemoveObject(Node2D obj)
        {
            return grid.RemoveObject(obj);
        }

        public bool MoveObject(Node2D obj, Vector2I pos)
        {
            return grid.MoveObject(obj, pos);
        }

        public bool MoveObject(Vector2I from, Vector2I to)
        {
            return grid.MoveObject(from, to);
        }

        public bool MoveObjectInDirection(Node2D obj, Vector2 dir)
        {
            return grid.MoveObjectInDirection(obj, dir);
        }

        public bool MoveObjectInDirection(Vector2I pos, Vector2 dir)
        {
            return grid.MoveObjectInDirection(pos, dir);
        }

        public Vector2I GetObjectPosition(Node2D obj)
        {
            return grid.GetObjectPosition(obj);
        }
    }
}
