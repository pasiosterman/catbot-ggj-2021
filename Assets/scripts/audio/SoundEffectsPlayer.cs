using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ2021
{
    public class SoundEffectsPlayer : MonoBehaviour, IStartup
    {
        public SoundEffect[] effects;
        protected AudioSource _audioSource;
        public float fadeDuration = 0.3f;

        private float _t = 0.0f;
        protected bool _fadingOut = false;
        protected bool _fadingIn = false;
        protected float _defaultVolume = 1.0f;

        public virtual void Stop()
        {
            if (_audioSource == null) return;
            _audioSource.Stop();
        }

        public virtual void FadeOut()
        {
            _t = 0.0f;
            _fadingOut = true;
            _fadingIn = false;
        }

        public virtual void FadeIn()
        {
            _t = 0.0f;
            _fadingOut = false;
            _fadingIn = true;
        }

        private void Update()
        {
            if (_fadingOut)
                TickFadeOut();
            else if (_fadingIn)
                TickFadeIn();
        }

        void TickFadeOut()
        {
            if (fadeDuration == 0)
                fadeDuration = 0.3f;

            _t += Time.deltaTime;
            if (_t > fadeDuration)
            {
                _t = fadeDuration;
                _fadingOut = false;
                Stop();
            }
            _audioSource.volume = _defaultVolume * (1.0f - (_t / fadeDuration));
        }

        void TickFadeIn()
        {
            if (fadeDuration == 0)
                fadeDuration = 0.3f;

            _t += Time.deltaTime;
            if (_t > fadeDuration)
            {
                _t = fadeDuration;
                _fadingIn = false;
            }
            _audioSource.volume = _defaultVolume * (_t / fadeDuration);
        }

        protected AudioClip GetClipByID(string id)
        {
            for (int i = 0; i < effects.Length; i++)
            {
                if (effects[i].id == id)
                {
                    return effects[i].clip;
                }
            }
            return null;
        }

        public void Startup()
        {
            _audioSource = GetComponent<AudioSource>();

            if (_audioSource != null)
                _defaultVolume = _audioSource.volume;
            else
                Debug.Log(LogTags.SYSTEM_ERROR + "missing audio source!", this);
        }
    }

    [System.Serializable]
    public class SoundEffect
    {
        public string id;
        public AudioClip clip;
    }
}