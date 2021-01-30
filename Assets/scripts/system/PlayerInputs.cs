using UnityEngine;

namespace GGJ2021
{
    public class PlayerInputs : MonoBehaviour, IStartup
    {
        private BehaviorStartup _behaviorStartup;
        public bool InputLock { get; set; }

        public Vector3 MovementInput { get; private set; }
        public bool RunInput { get; private set; }
        public bool JumpInput { get; private set; }

        public void Startup()
        {
            _behaviorStartup = GetComponent<BehaviorStartup>();
            RoboGame.AddTool(GameTools.PlayerInputs, this);
        }

        public void Update()
        {
            if (_behaviorStartup == null) return;
            if (!_behaviorStartup.Ready) return;
            if (RoboGame.PlayerMover == null) return;

            if(Input.GetKey(KeyCode.Escape))
                Application.Quit();

            if (!InputLock)
            {
                MovementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
                RunInput = Input.GetButton("Run");
                JumpInput = Input.GetButton("Jump");

                if (RoboGame.CameraChanger != null)
                {
                    if (Input.GetKey(KeyCode.F1))
                        RoboGame.CameraChanger.ChangeCamera(CameraChanger.AvailableCameras.Overhead);
                    else if (Input.GetKey(KeyCode.F2))
                        RoboGame.CameraChanger.ChangeCamera(CameraChanger.AvailableCameras.FirstPerson);
                }
            }
            else
            {
                MovementInput = Vector3.zero;
                RunInput = false;
                JumpInput = false;
            }

            RoboGame.PlayerMover.DesiredMovement = MovementInput;
            RoboGame.PlayerMover.WantsToRun = RunInput;
            RoboGame.PlayerMover.WantsToJump = JumpInput;
        }
    }
}
