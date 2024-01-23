using Godot;

namespace Gamelogic.Grid
{
    [GlobalClass]
    public partial class GodotGridNavigationAgent : Node, IGridNavigationAgent
    {
        private IGridNavigationAgent agent = null;
        private Node2D obj;

        [Export(PropertyHint.Enum, "AStar")]
        public string Agent {get; set;}

        [Export]
        public int Depth {get; set;} = 20;

        public Vector2I GetNextPosition(Vector2I target)
        {
            if (agent == null)
                return Vector2I.Zero;
            else
                return agent.GetNextPosition(target);

        }

        public Vector2I[] GetPathTo(Vector2I target)
        {
            if (agent == null)
                return default;
            else
                return agent.GetPathTo(target);
        }

        public override void _Ready()
        {
            obj = GetParent<Node2D>();

            agent = Agent switch
            {
                "AStar" => new AStarNavigationAgent(GameManager.Grid, obj, Depth),
                _ => null
            };
            base._Ready();
        }
    }
}