using UnityEngine;
using GGJ2021.MovementEvents;
using PUnity.EventHandling;

namespace GGJ2021
{
    public class PlayerActionsAudio : MonoBehaviour, IEventListener<MovementEventArgs>, IStartup
    {
        private Mover _mover;
        private OneShotSoundEffectPlayer _effectPlayer;

        public void HandleEvent(object sender, MovementEventArgs eventArgs)
        {
            if (eventArgs.GetType() == typeof(LandedEventArgs))
            {
                if (Time.time < 0.3f)
                    return;

                _effectPlayer.PlayOneShot("landed");
            }
        }

        public void Startup()
        {
            _mover = GetComponentInParent<Mover>();
            _mover.Attach(this);
            _effectPlayer = GetComponent<OneShotSoundEffectPlayer>();
        }
    }
}

