using Godot;

namespace Gamelogic.Grid
{
    /// <summary>
    /// Agent that uses AStar search with a distance heuristic to pathfind
    /// </summary>
    public class AStarNavigationAgent : IGridNavigationAgent
    {
        public Vector2I GetNextPosition(Vector2I target)
        {
            throw new System.NotImplementedException();
        }

        public Vector2I[] GetPathTo(Vector2I target)
        {
            throw new System.NotImplementedException();
        }
    }
}