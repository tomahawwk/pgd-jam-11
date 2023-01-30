using UnityEngine;

namespace Prototype.PlayerPhysics.Sounds
{
    [RequireComponent(typeof(Animator), typeof(AudioSource))]
    public class HumanoidAnimatorSounds : MonoBehaviour
    {
        [SerializeField] private PlayerBody _playerBody;
        private AudioSource _audioSource;
        
        private void Awake() => _audioSource = GetComponent<AudioSource>();
        private void OnEnable() => _playerBody.OnGround += OnGround;
        private void OnDisable() => _playerBody.OnGround -= OnGround;

        private SoundPlatform _soundPlatform;

        private void OnGround(Collider collider)
        {
            if (collider == null) return;
            
            var touchObject = collider.gameObject;

            _soundPlatform = null;
            
            if (touchObject.TryGetComponent(out SoundPlatform soundPlatform))
                _soundPlatform = soundPlatform;
        }

        public void Footstep()
        {
            if (_soundPlatform == null) return;
            
            _audioSource.clip = _soundPlatform.GetRandomSound();
            _audioSource.Play();
        }
    }
}