using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ2021
{
    public class TestScript : MonoBehaviour, IStartup
    {
        public float speed = 10;

        public bool Started { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        public void Startup()
        {
            Debug.Log(GetType().Name + "started");
        }

        private void Update()
        {
            transform.Rotate(0, speed * Time.deltaTime, 0);
        }
    }
}