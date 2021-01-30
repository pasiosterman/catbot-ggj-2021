using UnityEngine;

namespace GGJ2021
{
    public class CameraChanger : MonoBehaviour, IStartup
    {
        public Camera overheadCamera;
        public Camera fpsCamera;

        public AvailableCameras CurrentCamera { get; private set; }

        public void Startup()
        {
            RoboGame.AddTool(GameTools.CameraChanger, this);
            ChangeCamera(AvailableCameras.Overhead);
        }

        public void ChangeCamera(AvailableCameras targetCamera)
        {
            if (CurrentCamera == targetCamera)
                return;

            CurrentCamera = targetCamera;
            switch (targetCamera)
            {
                case AvailableCameras.Overhead:
                    overheadCamera.gameObject.SetActive(true);
                    fpsCamera.gameObject.SetActive(false);
                    break;
                case AvailableCameras.FirstPerson:
                    overheadCamera.gameObject.SetActive(false);
                    fpsCamera.gameObject.SetActive(true);
                    break;
                default:
                    overheadCamera.gameObject.SetActive(true);
                    fpsCamera.gameObject.SetActive(true);
                    break;
            }
        }
        public enum AvailableCameras { Overhead, FirstPerson }
    }
}