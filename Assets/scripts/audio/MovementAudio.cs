using UnityEngine;
using PUnity.EventHandling;
using GGJ2021.MovementEvents;

namespace GGJ2021
{
    public class MovementAudio : MonoBehaviour, IEventListener<MovementEventArgs>, IStartup
    {
        private Mover _mover;
        private ContinousSoundEffectPlayer _effectPlayer;

        public void HandleEvent(object sender, MovementEventArgs eventArgs)
        {
            if (eventArgs.GetType() == typeof(StartedMoving))
            {
                Debug.Log("STARTED MOVING?");
                _effectPlayer.PlayContinous("move");
                _effectPlayer.FadeIn();
            }
            else if (eventArgs.GetType() == typeof(StoppedMoving))
            {
                _effectPlayer.FadeOut();
            }
        }

        public void Startup()
        {
            _mover = GetComponentInParent<Mover>();
            _mover.Attach(this);
            _effectPlayer = GetComponent<ContinousSoundEffectPlayer>();
        }
    }
}


