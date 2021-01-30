using UnityEngine;

namespace GGJ2021
{
    public class CollisionAudio : MonoBehaviour, IStartup
    {
        private OneShotSoundEffectPlayer _effectPlayer;

        private float _timeStamp = -100.0f;

        public void Startup()
        {
            _effectPlayer = GetComponent<OneShotSoundEffectPlayer>();
        }

        public void SetCooldownTimestamp()
        {
            _timeStamp = Time.time;
        }

        private void OnCollisionEnter(Collision other)
        {
            if(Time.time < 0.3) return;

            if (_effectPlayer != null && (Time.time - _timeStamp) > 3.0f)
            {
                _effectPlayer.PlayRandomOneShot();
                SetCooldownTimestamp();
            }
        }
    }
}