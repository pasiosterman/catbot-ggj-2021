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
        public float TurnInput { get; private set; }

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

            if (Input.GetKey(KeyCode.Escape))
                Application.Quit();

            if (!InputLock)
            {
                MovementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
                RunInput = Input.GetButton("Run");
                JumpInput = Input.GetButton("Jump");

                TurnInput = Input.GetAxis("Turn x");

                if (RoboGame.CameraChanger != null)
                {
                    if (Input.GetKey(KeyCode.F1))
                        RoboGame.CameraChanger.ChangeCamera(CameraChanger.AvailableCameras.Overhead);
                    else if (Input.GetKey(KeyCode.F2))
                        RoboGame.CameraChanger.ChangeCamera(CameraChanger.AvailableCameras.FirstPerson);
                    else if (Input.GetKey(KeyCode.F3))
                        RoboGame.CameraChanger.ChangeCamera(CameraChanger.AvailableCameras.ThirdPerson);
                    else if (Input.GetKey(KeyCode.F4))
                        RoboGame.CameraChanger.ChangeCamera(CameraChanger.AvailableCameras.Ortoh);

                }
            }
            else
            {
                MovementInput = Vector3.zero;
                RunInput = false;
                JumpInput = false;
                TurnInput = 0.0f;
            }

            RoboGame.PlayerMover.DesiredMovement = MovementInput;
            RoboGame.PlayerMover.WantsToRun = RunInput;
            RoboGame.PlayerMover.WantsToJump = JumpInput;
            RoboGame.PlayerMover.DesiredTurning = TurnInput;
        }
    }
}
