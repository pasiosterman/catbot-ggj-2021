using UnityEngine;

namespace GGJ2021
{
    public class Cat : MonoBehaviour, IStartup, IPickable
    {
        public Collider[] colliders;

        private Rigidbody _rb;
        private OneShotSoundEffectPlayer _soundEffectPlayer;
        private CollisionAudio _collisionAudio;

        public bool CanPickUp { get { return true; } }
        public bool IsHeavy { get { return false; } }

        public void OnPickUp()
        {
            SetCollision(false);
            _soundEffectPlayer.PlayOneShot("on-hit");
        }

        public void OnPutDown()
        {
            SetCollision(true);
            _soundEffectPlayer.PlayOneShot("on-hit-2");
            _collisionAudio.SetCooldownTimestamp();
        }

        void SetCollision(bool enabled)
        {
            _rb.isKinematic = !enabled;
            _rb.useGravity = enabled;
            _rb.detectCollisions = enabled;
            for (int i = 0; i < colliders.Length; i++)
                colliders[i].enabled = enabled;
        }

        public void Startup()
        {
            _rb = GetComponent<Rigidbody>();
            _soundEffectPlayer = GetComponent<OneShotSoundEffectPlayer>();
            _collisionAudio = GetComponent<CollisionAudio>();
        }
    }
}

