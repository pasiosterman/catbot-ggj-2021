using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PUnity.EventHandling;
using GGJ2021.MovementEvents;
using System;

namespace GGJ2021
{
    public class Mover : MonoBehaviour, IStartup, IEventSubject<MovementEventArgs>
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
        private Vector3 _previousVelocity = Vector3.zero;

        public Vector3 DesiredMovement { get; set; } = Vector3.zero;
        public Vector3 MovementDirection { get; private set; }
        public float DesiredTurning { get; set; } = 0.0f;
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
            _groundedScanner.OnLandedEvent = OnLanded;
        }

        private void OnLanded()
        {
            SendEvent(new LandedEventArgs());
        }

        public void ChangeState(BaseMovementState newState)
        {
            CurrentState?.Exit();
            CurrentState = newState;
            CurrentState?.SetContext(this);
            CurrentState?.Entry();
        }

        private void FixedUpdate()
        {
            if (_behaviorStartup == null) return;
            if (!_behaviorStartup.Ready) return;

            CurrentState?.FixedExecute();

            if (_jumpDesireReleased && !WantsToJump)
                _jumpDesireReleased = false;
        }

        float _jumpDelay = 0.0f;

        private void Update()
        {
            CurrentState?.Execute();

            if (_jumpDelay > 0.0f)
                _jumpDelay -= Time.smoothDeltaTime;
        }

        public void Turn(float value)
        {
            transform.Rotate(0, value * turningSpeed * Time.fixedDeltaTime, 0);
        }

        public void Move(Vector3 direction, bool run = false)
        {
            if (_rb == null) return;

            direction = transform.TransformDirection(direction);
            Vector3 desiredVelocity = direction * speed;

            if (run) desiredVelocity *= runModifier;

            var velocity = _rb.velocity;
            var velocityChange = desiredVelocity - velocity;

            velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
            velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
            velocityChange.y = 0;
            _rb.AddForce(velocityChange, ForceMode.VelocityChange);

            if (desiredVelocity.magnitude > 0.0f)
                MovementDirection = desiredVelocity.normalized;

            if (IsGrounded && _jumpDelay <= 0)
            {
                if (_previousVelocity.magnitude == 0 && desiredVelocity.magnitude > 0)
                    SendEvent(new StartedMoving());
                else if (_previousVelocity.magnitude > 0 && desiredVelocity.magnitude == 0)
                    SendEvent(new StoppedMoving());

                _previousVelocity = desiredVelocity;
            }
        }

        public void Jump()
        {
            if (IsGrounded && !_jumpDesireReleased && _jumpDelay <= 0.0f)
            {
                _rb.velocity = new Vector3(_rb.velocity.x, jumpForce, _rb.velocity.z);
                _jumpDesireReleased = true;
                SendEvent(new Jumped());
                SendEvent(new StoppedMoving());
                _previousVelocity = Vector3.zero;
                _jumpDelay = 0.3f;
            }
        }

        private GenericEventHandler<MovementEventArgs> _movementEventHandler;
        private GenericEventHandler<MovementEventArgs> MovementEventHandler
        {
            get
            {
                if (_movementEventHandler == null)
                    _movementEventHandler = new GenericEventHandler<MovementEventArgs>(this);

                return _movementEventHandler;
            }
        }

        public void Attach(IEventListener<MovementEventArgs> listener)
        {
            MovementEventHandler.Attach(listener);
        }

        public void Detach(IEventListener<MovementEventArgs> listener)
        {
            MovementEventHandler.Detach(listener);
        }

        public void SendEvent(MovementEventArgs args)
        {
            MovementEventHandler.SendEvent(args);
        }
    }
}