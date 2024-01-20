using System;
using Godot;

namespace Gamelogic.Grid
{
    public class GridException : Exception
    {
        public Vector2I pos;
        public Node2D obj;

        public GridException(string message, Vector2I pos, Node2D obj)
            : base($"Grid Excpetion at {pos}, on object {obj.Name} : {message}")
        {
            this.pos = pos;
            this.obj = obj;
        }

        public GridException(string message, Vector2I pos)
            : base($"Grid Excpetion at {pos} : {message}")
        {
            this.pos = pos;
            obj = null;
        }

        public GridException(string message, Node2D obj)
            : base($"Grid Excpetion on object {obj} : {message}")
        {
            this.obj = obj;
            pos = Vector2I.Zero;
        }
    }
}