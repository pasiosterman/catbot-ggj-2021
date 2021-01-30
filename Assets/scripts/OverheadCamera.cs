using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ2021
{
    public class OverheadCamera : MonoBehaviour
    {
        public Vector3 offset = new Vector3(0, 2, -10);
        public float smoothTime = 0.3f;
        public bool lookat = false;

        private Vector3 _velocity = Vector3.zero;

        private void LateUpdate()
        {
            CameraUpdate(Time.deltaTime);
        }

        private void CameraUpdate(float deltaTime)
        {
            if (RoboGame.Player == null) return;
            Target = RoboGame.Player.transform;
            Vector3 targetPosition = Target.position + offset;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, smoothTime);

            if(lookat)
                transform.LookAt(Target);
        }

        public Transform Target { get; private set; }

    }
}


