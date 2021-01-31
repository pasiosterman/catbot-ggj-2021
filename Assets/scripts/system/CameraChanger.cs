using UnityEngine;

namespace GGJ2021
{
    public class CameraChanger : MonoBehaviour, IStartup
    {
        public Camera overheadCamera;
        public Camera fpsCamera;
        public Camera orthoCamera;
        public Camera thirdPersonCamera;
        public GameObject detachedHeadCamera;
        public GameObject catdropzonegraphic;

        public AvailableCameras CurrentCamera { get; private set; } = AvailableCameras.None;

        public void Startup()
        {
            RoboGame.AddTool(GameTools.CameraChanger, this);
        }

        public void ChangeCamera(AvailableCameras targetCamera)
        {
            string currentCameraName = "none";

            if(currentCameraName != null)
                currentCameraName = CurrentCamera.ToString();

            Debug.Log(currentCameraName + " => " + targetCamera.ToString() );

            detachedHeadCamera.SetActive(false);
            catdropzonegraphic.SetActive(true);
            
            CurrentCamera = targetCamera;
            switch (targetCamera)
            {
                case AvailableCameras.Overhead:
                    overheadCamera.enabled = true;
                    fpsCamera.enabled = false;
                    orthoCamera.enabled = false;
                    thirdPersonCamera.enabled = false;
                    break;
                case AvailableCameras.FirstPerson:
                    overheadCamera.enabled = false;
                    fpsCamera.enabled = true;
                    orthoCamera.enabled = false;
                    thirdPersonCamera.enabled = false;
                    break;
                case AvailableCameras.Ortoh:
                    overheadCamera.enabled = false;
                    fpsCamera.enabled = false;
                    orthoCamera.enabled = true;
                    thirdPersonCamera.enabled = false;
                    break;
                case AvailableCameras.ThirdPerson:
                    overheadCamera.enabled = false;
                    fpsCamera.enabled = false;
                    orthoCamera.enabled = false;
                    thirdPersonCamera.enabled = true;
                    break;
                default:
                    overheadCamera.enabled = true;
                    fpsCamera.enabled = false;
                    orthoCamera.enabled = false;
                    thirdPersonCamera.enabled = false;
                    break;
            }
        }
        public enum AvailableCameras { None, Overhead, FirstPerson, Ortoh, ThirdPerson }
    }
}