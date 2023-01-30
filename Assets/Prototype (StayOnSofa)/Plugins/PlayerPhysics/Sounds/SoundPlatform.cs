using UnityEngine;

namespace Prototype.PlayerPhysics.Sounds
{
    public class SoundPlatform : MonoBehaviour
    {
        [SerializeField] private FootSoundContainer _container;

        public AudioClip GetRandomSound()
        {
            return _container.GetRandomAudio();
        }
    }
}