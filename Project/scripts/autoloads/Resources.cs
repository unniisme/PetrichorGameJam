using Godot;

namespace Gamelogic
{
    public partial class Resources : Node
    {
        public const string hudScenePath = "res://scenes/UI/hud.tscn";
        private readonly static PackedScene hudScene = ResourceLoader.Load<PackedScene>(hudScenePath);
        public static PackedScene HudScene => hudScene;
    }
}