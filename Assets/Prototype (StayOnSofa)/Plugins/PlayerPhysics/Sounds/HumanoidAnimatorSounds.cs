using Prototype.PlayerPhysics.Graphics;
using UnityEngine;

namespace Prototype.PlayerPhysics.Sounds
{
    [RequireComponent(typeof(AudioSource), typeof(HumanoidBodyGraphics))]
    public class HumanoidAnimatorSounds : MonoBehaviour
    {
        private HumanoidBodyGraphics _humanoid => GetComponent<HumanoidBodyGraphics>();
        
        private AudioSource _audioSource;
        private SoundPlatform _soundPlatform;
        private void Awake() => _audioSource = GetComponent<AudioSource>();

        private void OnEnable()
        {
            _humanoid.PlayerBody.OnGround += OnGround;
            _humanoid.OnHumanoidStep += FootStepL;
        }

        private void OnDisable()
        {
            _humanoid.PlayerBody.OnGround -= OnGround;
            _humanoid.OnHumanoidStep -= FootStepL;
        }


        private void OnGround(Collider collider)
        {
            if (collider == null) return;

            var touchObject = collider.gameObject;

            _soundPlatform = null;

            if (touchObject.TryGetComponent(out SoundPlatform soundPlatform))
                _soundPlatform = soundPlatform;
        }

        private void FootStepL()
        {
            if (_humanoid.GetLerp() > 0.5f)
            {
                if (_soundPlatform == null) return;

                _audioSource.clip = _soundPlatform.GetRandomSoundL();
                _audioSource.Play();
            }
        }
        private void FootStepR()
        {
            if (_humanoid.GetLerp() > 0.5f)
            {
                if (_soundPlatform == null) return;

                _audioSource.clip = _soundPlatform.GetRandomSoundR();
                _audioSource.Play();
            }
        }
    }
}