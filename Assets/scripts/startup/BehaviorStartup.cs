using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ2021
{
    public class BehaviorStartup : MonoBehaviour
    {
        public enum StartupPriority
        {
            Early = 0,
            Default = 1,
            Late = 2
        }

        public StartupPriority Priority = StartupPriority.Default;


        public void Startup()
        {
            Debug.Log(name + " startup!");
            IStartup[] objs = GetComponentsInChildren<IStartup>();
            for (int i = 0; i < objs.Length; i++)
            {
                objs[i].Startup();
            }

            Ready = true;
        }
        public bool Ready { get; private set; }
    }
}