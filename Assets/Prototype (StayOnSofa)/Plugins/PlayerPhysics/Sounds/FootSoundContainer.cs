using System.Collections.Generic;
using UnityEngine;

namespace Prototype.PlayerPhysics.Sounds
{
    [CreateAssetMenu(fileName = "FootSounds", menuName = "Sounds/FootSounds", order = 1)]

    public class FootSoundContainer : ScriptableObject
    {
        [SerializeField] private List<AudioClip> _clips_L;
        [SerializeField] private List<AudioClip> _clips_R;

        public AudioClip GetRandomAudioL()
        {
            return _clips_L[Random.Range(0, _clips_L.Count)];
        }

        public AudioClip GetRandomAudioR()
        {
            return _clips_R[Random.Range(0, _clips_R.Count)];
        }
    }
}