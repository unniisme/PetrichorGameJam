using System;
using Godot;

namespace Gamelogic.Grid
{
    /// <summary>
    /// A test grid that only has 1 object
    /// </summary>
    public class MockGrid : IGrid
    {
        private Node2D obj = null;
        private Vector2I objPos;

        public Vector2 Scale { get; set; }
        public Vector2 Offset { get; set; }

        public MockGrid(Vector2 scale, Vector2 offset)
        {
            Scale = scale;
            Offset = offset;
        }

        public MockGrid(Vector2 scale)
        {
            Scale = scale;
            Offset = Vector2.Zero;
        }

        public MockGrid()
        {
            Scale = Vector2.One;
            Offset = Vector2.Zero;
        }

        public Vector2I GameCoordinateToGridCoordinate(Vector2 pos)
        {
            return (Vector2I)((pos - Offset)/Scale).Round();
        }

        public Node2D GetObject(Vector2I pos)
        {
            if (objPos == pos) return obj;
            return null;
        }

        public Vector2I GetObjectPosition(Node2D obj)
        {
            if (obj == this.obj) return objPos;
            throw new Exception("Object not present in grid");
        }

        public Vector2 GridCoordinateToGameCoordinate(Vector2I pos)
        {
            return ((Vector2)pos * Scale) + Offset;
        }

        public bool MoveObject(Node2D obj, Vector2I pos)
        {
            if (RemoveObject(obj))
            {
                return PlaceObject(obj, pos);
            }
            return false;
        }

        public bool MoveObject(Vector2I from, Vector2I to)
        {
            if (RemoveObject(from))
            {
                return PlaceObject(obj, to);
            }
            return false;

        }

        public bool MoveObjectInDirection(Node2D obj, Vector2 dir)
        {
            Vector2I targetPos = (Vector2I)dir.Normalized().Round();
            return MoveObject(obj, objPos+targetPos);
        }

        public bool MoveObjectInDirection(Vector2I pos, Vector2 dir)
        {
            return MoveObjectInDirection(GetObject(pos), dir);
        }

        public bool PlaceObject(Node2D obj, Vector2I pos)
        {
            this.obj = obj;
            objPos = pos;
            return true;
        }

        public bool RemoveObject(Vector2I pos)
        {
            if (pos == objPos)
            {
                obj = null;
                return true;
            }
            return false;
        }

        public bool RemoveObject(Node2D obj)
        {
            if (obj == this.obj)
            {
                obj = null;
                return true;
            }
            return false;
        }
    }
}