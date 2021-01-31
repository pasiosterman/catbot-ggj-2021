using UnityEngine;

namespace GGJ2021
{
    public class ViewModule : RobotModule
    {
        public GameObject partGameObject;

        public RobotModules moduleType = RobotModules.OverheadModule;
        public override RobotModules ModuleType { get { return moduleType; } }

        private void OnEnable()
        {
            Player player = GetComponentInParent<Player>();
            switch (moduleType)
            {
                case RobotModules.OverheadModule:
                    player.OverheadViewEnabled = true;
                    RoboGame.CameraChanger.ChangeCamera(CameraChanger.AvailableCameras.Overhead);
                    break;
                case RobotModules.FpsModule:
                    player.FirstPersonViewEnabled = true;
                    break;
                case RobotModules.ThirdPersonModule:
                    player.ThirdPersonViewEnabled = true;
                    break;
                case RobotModules.OrthoModule:
                    player.OrthoViewEnabled = true;
                    break;
                default:
                    break;
            }
            partGameObject?.SetActive(true);
        }
    }
}