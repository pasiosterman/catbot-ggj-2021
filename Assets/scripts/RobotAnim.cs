using System;
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
        private Player _player;
        private Vector3 _velocity = Vector3.zero;

        public Transform leftHandTransform;
        public Transform rightHandTransform;

        public void Startup()
        {
            _mover = GetComponentInParent<Mover>();
            _player = GetComponentInParent<Player>();
        }

        private void Update()
        {
            if (IsStrafeMovementActive())
                AnimateMovementDirection();

            if (_player.CarryObjectsModule != null)
                AnimateHandsForPickupModule();
        }

        private bool IsStrafeMovementActive()
        {
            return _mover.CurrentState.GetType() != typeof(TankMovementState);
        }

        private void AnimateHandsForPickupModule()
        {
            CarryObjectsModule module = _player.CarryObjectsModule as CarryObjectsModule;
            bool carryingObject = module.CurrentPickable != null;

            float lerpTime = 10.0f;
            if(carryingObject)
            {
                leftHandTransform.localRotation = Quaternion.Lerp(leftHandTransform.localRotation, Quaternion.Euler(-67.0f, 20.0f,0), Time.deltaTime * lerpTime);
                rightHandTransform.localRotation = Quaternion.Lerp(rightHandTransform.localRotation, Quaternion.Euler(-67.0f, -20.0f,0), Time.deltaTime * lerpTime);
            }
            else
            {
                 leftHandTransform.localRotation = Quaternion.Lerp(leftHandTransform.localRotation, Quaternion.identity, Time.deltaTime * lerpTime);
                rightHandTransform.localRotation = Quaternion.Lerp(rightHandTransform.localRotation, Quaternion.identity, Time.deltaTime * lerpTime);
            }
        }

        private void AnimateMovementDirection()
        {
            Vector3 direction = _mover.MovementDirection;
            Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
            feetTransform.rotation = QuaternionUtil.SmoothDampQuaternion(feetTransform.rotation, targetRotation, ref _velocity, 0.1f);

            if (_mover.DesiredMovement.magnitude > 0.0f)
                wheelTransform.Rotate(150 * Time.deltaTime, 0, 0);
        }
    }
}


