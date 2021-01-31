using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ2021
{
    public class FollowTransform : MonoBehaviour
    {
        public Transform targetTransform;
        Vector3 _velocity = Vector3.zero;
        Vector3 _rotVelocity = Vector3.zero;


        private void LateUpdate()
        {
            Vector3 targetPosition = targetTransform.position;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, 0.01f);
            transform.rotation = QuaternionUtil.SmoothDampQuaternion(transform.rotation, targetTransform.rotation, ref _rotVelocity, 0.01f);
        }

        

    }
}

