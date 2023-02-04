using UnityEngine;

namespace Prototype.PlayerPhysics.Sounds
{
    public class SoundPlatform : MonoBehaviour
    {
        [SerializeField] private FootSoundContainer _container;

        public AudioClip GetRandomSoundL()
        {
            return _container.GetRandomAudioL();
        }

        public AudioClip GetRandomSoundR()
        {
            return _container.GetRandomAudioR();
        }
    }
}