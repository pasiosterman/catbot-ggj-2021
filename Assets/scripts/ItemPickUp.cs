using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GGJ2021
{
    public class ItemPickUp : MonoBehaviour, IStartup
    {
        public UnityEvent pickedUpEvent;
        private OneShotSoundEffectPlayer _soundEffectPlayer;
        private bool _consumed = false;

        public void Startup()
        {
            _soundEffectPlayer = GetComponentInChildren<OneShotSoundEffectPlayer>();
        }

        public void Pickup()
        {
            _consumed = true;
            pickedUpEvent.Invoke();
            _soundEffectPlayer.PlayRandomOneShot();
            _soundEffectPlayer.transform.SetParent(null);
            Destroy(_soundEffectPlayer.gameObject, 5.0f);
            Destroy(gameObject, 0.1f);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_consumed)
                return;

            Player player = other.GetComponentInParent<Player>();
            if (player != null)
                Pickup();
        }
    }
}