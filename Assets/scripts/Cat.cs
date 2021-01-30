using UnityEngine;

namespace GGJ2021
{
    public class Cat : MonoBehaviour, IStartup, IPickable
    {
        Rigidbody _rb;
        public Collider[] colliders;

        public bool CanPickUp { get{ return true; }}
        public bool IsHeavy { get{ return false; }}

        public void OnPickUp()
        {
            _rb.detectCollisions = false;
            for (int i = 0; i < colliders.Length; i++)
            {
                colliders[i].enabled = false;
            }
        }

        void SetCollisers(bool enabled)
        {
            
        }

        public void OnPutDown()
        {
            _rb.detectCollisions = true;
        }

        public void Startup()
        {
            _rb = GetComponent<Rigidbody>();
        }
    }
}

