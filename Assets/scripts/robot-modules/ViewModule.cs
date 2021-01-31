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
                    RoboGame.TipWindow.AddTip("Press F3 to enter first person view, F1 to return to overhead view");
                    break;
                case RobotModules.ThirdPersonModule:
                    player.ThirdPersonViewEnabled = true;
                    RoboGame.TipWindow.AddTip("Press F2 to enter third person view, F1 to return to overhead view" );
                    break;
                case RobotModules.OrthoModule:
                    player.OrthoViewEnabled = true;
                    RoboGame.TipWindow.AddTip("Press F4 to enter orthographic view, F1 to return to overhead view");
                    break;
                default:
                    break;
            }
            partGameObject?.SetActive(true);
        }
    }
}