using UnityEngine;

namespace GGJ2021
{
    public class Cat : MonoBehaviour, IStartup
    {
        Rigidbody _rb;
        public void Startup()
        {
            _rb = GetComponent<Rigidbody>();
        }
    }
}

