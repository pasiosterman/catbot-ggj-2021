using UnityEngine;

namespace GGJ2021
{
    public class IsGroundedScanner : MonoBehaviour
    {
        private int _groundCount = 0;
        public int GroundCount { get { return _groundCount; } }
        public bool IsGrounded { get { return _groundCount > 0; } }

        private void OnTriggerEnter(Collider other)
        {
            _groundCount++;
        }

        private void OnTriggerExit(Collider other)
        {
            _groundCount--;
        }
    }
}