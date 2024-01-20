using System.Collections.Generic;
using Godot;

namespace Gamelogic.Grid
{
    public class GodotGrid : IGrid
    {
        private readonly Dictionary<Node2D, Vector2I> objectToIndex = new();
        private readonly Dictionary<Vector2I, Node2D> indexToObject = new();

        public Vector2 Scale { get; set; }
        public Vector2 Offset { get; set; }

        public GodotGrid(Vector2 scale, Vector2 offset)
        {
            Scale = scale;
            Offset = offset;
        }

        public GodotGrid(Vector2 scale)
        {
            Scale = scale;
            Offset = Vector2.Zero;
        }

        public GodotGrid()
        {
            Scale = Vector2.One;
            Offset = Vector2.Zero;
        }

        public Vector2I GameCoordinateToGridCoordinate(Vector2 pos)
        {
            return (Vector2I)((pos - Offset)/Scale).Round();
        }
        public Vector2 GridCoordinateToGameCoordinate(Vector2I pos)
        {
            return ((Vector2)pos * Scale) + Offset;
        }

        public Node2D GetObject(Vector2I pos)
        {
            if (indexToObject.ContainsKey(pos))
            {
                return indexToObject[pos];
            }
            throw new GridException("No object at position", pos);
        }

        public Vector2I GetObjectPosition(Node2D obj)
        {
            if (objectToIndex.ContainsKey(obj))
            {
                return objectToIndex[obj];
            }
            throw new GridException("Object not present in grid", obj);
        }

        public Vector2 GetObjectPositionInGameCoordinates(Node2D obj)
        {
            return GridCoordinateToGameCoordinate(GetObjectPosition(obj));
        }

        public void PlaceObject(Node2D obj, Vector2I pos)
        {
            if (objectToIndex.ContainsKey(obj))
                throw new GridException("Object is already present in grid", pos, obj);
            if (indexToObject.ContainsKey(pos))
                throw new GridException("Position is already occupied", pos, obj);

            objectToIndex[obj] = pos;
            indexToObject[pos] = obj;
        }

        public void PlaceObject(Node2D obj)
        {
            PlaceObject(obj, GameCoordinateToGridCoordinate(obj.GlobalPosition));
        }
        public void MoveObject(Node2D obj, Vector2I pos)
        {
            if (indexToObject.ContainsKey(pos))
            {
                throw new GridException("Position is already occupied", pos, obj);
            }
            RemoveObject(obj);
            PlaceObject(obj, pos);
        }

        public void MoveObject(Vector2I from, Vector2I to)
        {
            Node2D obj = GetObject(from);
            MoveObject(obj, to);
        }

        public void MoveObjectInDirection(Node2D obj, Vector2 dir)
        {
            Vector2I targetPos = (Vector2I)dir.Normalized().Round();
            MoveObject(obj, GetObjectPosition(obj)+targetPos);
        }

        public void MoveObjectInDirection(Vector2I pos, Vector2 dir)
        {
            Vector2I targetPos = (Vector2I)dir.Normalized().Round();
            MoveObject(pos, pos+targetPos);
        }


        public void RemoveObject(Vector2I pos)
        {
            if (!indexToObject.ContainsKey(pos))
            {
                throw new GridException("Position not occupied", pos);
            }
            Node2D obj = indexToObject[pos];
            indexToObject.Remove(pos);
            objectToIndex.Remove(obj);
        }

        public void RemoveObject(Node2D obj)
        {
            if (!objectToIndex.ContainsKey(obj))
            {
                throw new GridException("Object not present in grid", obj);
            }
            Vector2I pos = objectToIndex[obj];
            objectToIndex.Remove(obj);
            indexToObject.Remove(pos);
        }
    }
}