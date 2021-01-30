using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ2021
{
    public class ContinousSoundEffectPlayer : SoundEffectsPlayer, IStartup
    {
        public void PlayContinous(string id)
        {
            if (_audioSource == null) return;

            _fadingOut = false;
            _audioSource.volume = _defaultVolume;

            AudioClip clip = GetClipByID(id);
            if (clip != null)
            {
                _audioSource.clip = clip;
                _audioSource.loop = true;
                _audioSource.Play();
            }
        }

        [ContextMenu("Play random")]
        public void TestPlayRandom()
        {
            if (effects.Length == 0) return;
            int index = 0;
            PlayContinous(effects[index].id);
        }
    }
}