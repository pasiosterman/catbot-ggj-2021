using PUnity;

namespace GGJ2021
{
    public static class RoboGame
    {
        public static Player Player { get { return GetTool<Player>(GameTools.Player); } }

        public static void AddTool(GameTools key, object value){ tools.SetTool(key, value); }
        public static T GetTool<T>(GameTools key)
        {
            if (tools.ContainsTool(key))
                return tools.GetTool<T>(key);
            else
                return default;
        }
        public static bool HasTool(GameTools key) { return tools.ContainsTool(key); }
        public static ToolBox<GameTools> tools = new ToolBox<GameTools>();
    }

    public enum GameTools
    {
        PlayerInputs = 0,
        Player = 1
    } 
}