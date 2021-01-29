using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ggj2021
{
    public class TestScript : MonoBehaviour
    {
        public float speed = 10;

        private void Update()
        {
            transform.Rotate(0, speed * Time.deltaTime, 0);
        }
    }
}