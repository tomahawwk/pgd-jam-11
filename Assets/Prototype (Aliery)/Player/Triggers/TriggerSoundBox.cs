using Dialogue;
using UnityEngine;

namespace Prototype.Triggers.Types
{
    [RequireComponent(typeof(AudioSource))]
    public class TriggerSoundBox : MonoBehaviour
    {
        private BoxCollider _boxCollider => GetComponent<BoxCollider>();
        private void OnValidate() => _boxCollider.isTrigger = true;

        public AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void OnTriggerEnter(Collider other)
        {
            var touchObject = other.gameObject;

            if (touchObject.TryGetComponent(out Player _))
            {
                if (!_audioSource.isPlaying)
                _audioSource.PlayOneShot(_audioSource.clip, 0.7F);
                //Destroy(gameObject);
            }
        }
    }
}