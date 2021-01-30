using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ2021
{
    public class RobotAnim : MonoBehaviour, IStartup
    {
        public Transform feetTransform;
        Mover _mover;

        public void Startup()
        {
            _mover = GetComponentInParent<Mover>();
        }

        private void Update()
        {
            if (_mover.CurrentState.GetType() == typeof(TankMovementState))
                return;

            Vector3 direction = _mover.DesiredMovement;
        }
    }
}


