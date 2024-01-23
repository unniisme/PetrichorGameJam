using Godot;

namespace Gamelogic.Grid
{
	/// <summary>
	/// Agent that uses AStar search with a distance heuristic to pathfind
	/// </summary>
	public class AStarNavigationAgent : IGridNavigationAgent
	{
		private readonly IGrid grid;
		private readonly Node2D obj;
		private readonly int maxDepth;
		public AStarNavigationAgent(IGrid grid, Node2D obj, int maxDepth)
		{
			this.grid = grid;
			this.obj = obj;
			this.maxDepth = maxDepth;
		}
		
		public Vector2I GetNextPosition(Vector2I target)
		{
			Vector2I from = grid.GetObjectPosition(obj);
			// If the agent is already at the target position, no need to move
			if (from == target)
			{
				return from;
			}
			// Find the path using A*
			Vector2I[] path = GetPathTo(target);
			// If there is a path, return the next position in the path
			if (path.Length > 0)
			{
				return path[0];
			}
			// If no valid path is found, return the current position
			return from;
		}
		
		public Vector2I[] GetPathTo(Vector2I target)
		{
			Vector2I start = grid.GetObjectPosition(obj);
			// Priority queue for open set (nodes to be evaluated)
			PriorityQueue<Vector2I> openSet = new PriorityQueue<Vector2I>();
			// Set of evaluated nodes
			HashSet<Vector2I> closedSet = new HashSet<Vector2I>();
			// Dictionary to store the cost from start to each node
			Dictionary<Vector2I, float> gScore = new Dictionary<Vector2I, float>();
			// Dictionary to store the total cost from start to goal through each node
			Dictionary<Vector2I, float> fScore = new Dictionary<Vector2I, float>();

			openSet.Enqueue(start, 0);
			gScore[start] = 0;
			fScore[start] = HeuristicScore(start, target);
			while (openSet.Count > 0)
			{
				Vector2I current = openSet.Dequeue();
				// If we reached the target
				if (current == target)
				{
					// Reconstruct the path
					return ReconstructPath(current);
				}
				// to not be further evaluated
				closedSet.Add(current);
				foreach (Vector2I neighbor in FindValidNeighbors(current, target))
				{
					if (closedSet.Contains(neighbor))
					{
						continue; // Ignore the neighbor which is already evaluated
					}
					float tentativeGScore = gScore[current] + 1; // Assuming each step has a cost of 1
					if (!openSet.Contains(neighbor) || tentativeGScore < gScore[neighbor])
					{
						// This path to the neighbor is better than the previous one
						gScore[neighbor] = tentativeGScore;
						fScore[neighbor] = tentativeGScore + HeuristicScore(neighbor, target);
						if (!openSet.Contains(neighbor))
						{
							openSet.Enqueue(neighbor, fScore[neighbor]);
						}
					}
				}
			}
			// If no path is found
			return new Vector2I[0];
		}

		private Vector2I[] ReconstructPath(Vector2I current)
		{
			List<Vector2I> path = new List<Vector2I>();
			while (cameFrom.ContainsKey(current))
			{
				path.Add(current);
				current = cameFrom[current];
			}
			path.Reverse();
			return path.ToArray();
		}

		
		private FindValidNeighbors(Vector2I curr, Vector2I target)
		{
			List<Vector2I> validNeighbors = new List<Vector2I>();
			// Define the eight possible directions (8-way movement)
			int[] dx = { -1, 0, 1, 0, -1, 1, 1, -1 };
			int[] dy = { 0, 1, 0, -1, 1, 1, -1, -1 };

			for (int i = 0; i < 8; i++)
			{
				int newX = curr.x + dx[i];
				int newY = curr.y + dy[i];
				Vector2I newPos = new Vector2I(newX, newY);
				// Check if the new position is within the grid boundaries and is walkable
				if (IsWalkable(newPos, target))
				{
					validNeighbors.Add(newPos);
				}
			}
			return validNeighbors.ToArray();
		}

		private bool IsWalkable(Vector2I from, Vector2I target)
		{
			node2D obj = grid.GetObject(from);
			if(obj != null || HeuristicScore(from, target)>maxDepth)
			{
				return false;
			}
			return true;
		}
		
		
		
		private int HeuristicScore(Vector2I current, Vector2I target)
		{
			int dx = Math.Abs(current.x - target.x);
			int dy = Math.Abs(current.y - target.y);

			// Assuming D is a constant, you can adjust its value as needed
			int D = 1.0f;

			return D * (dx + dy);
		}
	}
}
