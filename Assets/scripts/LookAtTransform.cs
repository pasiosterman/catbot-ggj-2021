using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ2021
{
    public class LookAtTransform : MonoBehaviour
    {
        public Transform target;

        void LateUpdate()
        {
            transform.LookAt(target);
        }
    }
}
