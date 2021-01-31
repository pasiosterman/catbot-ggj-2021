using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ2021
{
    public class RobotAnim : MonoBehaviour, IStartup
    {
        public Transform feetTransform;
        public Transform wheelTransform;

        private Mover _mover;
        private Vector3 _velocity = Vector3.zero;
        private Vector3 _previousDirection;

        public void Startup()
        {
            _mover = GetComponentInParent<Mover>();
            _previousDirection = transform.forward;
        }

        private void Update()
        {
            if (_mover.CurrentState.GetType() == typeof(TankMovementState))
                return;

            Vector3 direction = _mover.MovementDirection;
            Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
            feetTransform.rotation = QuaternionUtil.SmoothDampQuaternion(feetTransform.rotation, targetRotation, ref _velocity, 0.1f);
            _previousDirection = direction;

            if(_mover.DesiredMovement.magnitude > 0.0f)
                wheelTransform.Rotate(150 * Time.deltaTime, 0,0);
        }
    }
}


