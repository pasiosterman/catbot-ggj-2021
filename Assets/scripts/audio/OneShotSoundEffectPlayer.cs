using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ2021
{
    public class OneShotSoundEffectPlayer : SoundEffectsPlayer, IStartup
    {
        public void PlayOneShot(string id)
        {
            if (_audioSource == null) return;

            AudioClip clip = GetClipByID(id);
            if (clip != null)
                _audioSource.PlayOneShot(clip);
            else
                Debug.LogWarning(LogTags.SYSTEM_ERROR + "No audio with id: " + id);
        }

        [ContextMenu("Play oneshot random")]
        public void PlayRandomOneShot()
        {
            if (effects.Length == 0) return;

            int index = Random.Range(0, effects.Length);
            PlayOneShot(effects[index].id);
        }
    }
}