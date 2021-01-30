using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ2021
{
    public class Mover : MonoBehaviour, IStartup
    {
        [Tooltip("Max speed in m/s")]
        public float speed = 5.0f;
        [Tooltip("Acceleration")]
        public float maxVelocityChange = 1.0f;
        [Tooltip("Straight multiplier i.e 2.0 = double the speed")]
        public float runModifier = 1.5f;
        [Tooltip("Degrees in second")]
        public float turningSpeed = 100.0f;

        public float jumpForce = 2.0f;

        private BehaviorStartup _behaviorStartup;
        private Rigidbody _rb;
        private IsGroundedScanner _groundedScanner;
        private bool _jumpDesireReleased = false;

        public Vector3 DesiredMovement { get; set; } = Vector3.zero;
        public bool WantsToRun { get; set; }
        public bool WantsToJump { get; set; }
        public bool IsGrounded
        {
            get
            {
                if (_groundedScanner != null)
                    return _groundedScanner.IsGrounded;
                else
                    return false;
            }
        }
        public BaseMovementState CurrentState { get; private set; }

        public void Startup()
        {
            _behaviorStartup = GetComponent<BehaviorStartup>();
            _rb = GetComponent<Rigidbody>();
            _groundedScanner = GetComponentInChildren<IsGroundedScanner>();
        }

        public void ChangeState(BaseMovementState newState)
        {
            CurrentState?.Exit();
            CurrentState = newState;
            CurrentState?.SetContext(this);
            CurrentState?.Entry();
        }

        public void FixedUpdate()
        {
            if (_behaviorStartup == null) return;
            if (!_behaviorStartup.Ready) return;

            CurrentState?.FixedExecute();

            if(_jumpDesireReleased && !WantsToJump)
                _jumpDesireReleased = false;
        }

        public void Turn(float value)
        {
            transform.Rotate(0, value * turningSpeed * Time.fixedDeltaTime, 0);
        }

        public void Move(Vector3 direction, bool run = false)
        {
            if (_rb == null) return;

            direction = transform.TransformDirection(direction);
            Vector3 desiredVeloicty = direction * speed;

            if (run) desiredVeloicty *= runModifier;

            var velocity = _rb.velocity;
            var velocityChange = desiredVeloicty - velocity;
            velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
            velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
            velocityChange.y = 0;
            _rb.AddForce(velocityChange, ForceMode.VelocityChange);
        }

        public void Jump()
        {
            if(IsGrounded && !_jumpDesireReleased)
            {
                _rb.velocity = new Vector3(_rb.velocity.x, jumpForce, _rb.velocity.z);
                _jumpDesireReleased = true;
            }
        }
    }
}