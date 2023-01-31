using System;
using System.Collections;
using UnityEngine;

namespace Prototype.Triggers.Types
{
    [RequireComponent(typeof(AudioSource))]
    public class TriggerSoundBox : OnPlayerTouchTrigger
    {
        private AudioSource _audioSource;
        private void Awake() => _audioSource = GetComponent<AudioSource>();

        public override void OnPlayerTouch()
        {
            if (!_audioSource.isPlaying)
                _audioSource.PlayOneShot(_audioSource.clip, 0.7F);
        }

        public override void Destroy()
        {
            StartCoroutine(RoutineSounds());
        }

        private IEnumerator RoutineSounds()
        {
            while (_audioSource.isPlaying)
                yield return null;

            Destroy(gameObject);
        }
    }
}