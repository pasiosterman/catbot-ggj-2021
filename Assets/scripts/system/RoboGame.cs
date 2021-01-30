using PUnity;

namespace GGJ2021
{
    public static class RoboGame
    {
        public static Player Player { get { return GetTool<Player>(GameTools.Player); } }
        public static Mover PlayerMover {get {return GetTool<Mover>(GameTools.PlayerMover); } }
        public static CameraChanger CameraChanger {get {return GetTool<CameraChanger>(GameTools.CameraChanger); } }

        public static void AddTool(GameTools key, object value){ UnityEngine.Debug.Log("set tool " + key); tools.SetTool(key, value); }
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
        Player = 1,
        PlayerMover = 2,
        CameraChanger = 3
    }
}