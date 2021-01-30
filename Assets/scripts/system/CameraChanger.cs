using UnityEngine;

namespace GGJ2021
{
    public class CameraChanger : MonoBehaviour, IStartup
    {
        public Camera overheadCamera;
        public Camera fpsCamera;
        public Camera orthoCamera;
        public Camera thirdPerson;

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
                    orthoCamera.gameObject.SetActive(false);
                    thirdPerson.gameObject.SetActive(false);
                    break;
                case AvailableCameras.FirstPerson:
                    overheadCamera.gameObject.SetActive(false);
                    fpsCamera.gameObject.SetActive(true);
                    orthoCamera.gameObject.SetActive(false);
                    thirdPerson.gameObject.SetActive(false);
                    break;
                case AvailableCameras.Ortoh:
                    overheadCamera.gameObject.SetActive(false);
                    fpsCamera.gameObject.SetActive(false);
                    orthoCamera.gameObject.SetActive(true);
                    thirdPerson.gameObject.SetActive(false);
                    break;
                case AvailableCameras.ThirdPerson:
                    overheadCamera.gameObject.SetActive(false);
                    fpsCamera.gameObject.SetActive(false);
                    orthoCamera.gameObject.SetActive(false);
                    thirdPerson.gameObject.SetActive(true);
                    break;
                default:
                    overheadCamera.gameObject.SetActive(true);
                    fpsCamera.gameObject.SetActive(false);
                    orthoCamera.gameObject.SetActive(false);
                    thirdPerson.gameObject.SetActive(false);
                    break;
            }
        }
        public enum AvailableCameras { Overhead, FirstPerson, Ortoh, ThirdPerson }
    }
}