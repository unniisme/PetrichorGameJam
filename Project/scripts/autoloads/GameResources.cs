using Godot;

namespace Gamelogic
{
    public partial class GameResources : Node
    {
        public const string hudScenePath = "res://scenes/UI/hud.tscn";
        private readonly static PackedScene hudScene = ResourceLoader.Load<PackedScene>(hudScenePath);
        public static PackedScene HudScene => hudScene;

        public const string bulletScenePath = "res://scenes/actors/bullet.tscn";
        private readonly static PackedScene bulletScene = ResourceLoader.Load<PackedScene>(bulletScenePath);
        public static PackedScene BulletScene => bulletScene;

        private readonly static string[] levels = 
        {
            "res://scenes/levels/Tutorial.tscn"
        };
        public static string[] Levels => levels;

        public const string mainMenuScene = "res://scenes/UI/MainMenu.tscn";
    }
}