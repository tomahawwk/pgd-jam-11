using System.Collections.Generic;
using UnityEngine;

namespace Prototype.PlayerPhysics.Sounds
{
    [CreateAssetMenu(fileName = "FootSounds", menuName = "Sounds/FootSounds", order = 1)]

    public class FootSoundContainer : ScriptableObject
    {
        [SerializeField] private List<AudioClip> _clips;

        public AudioClip GetRandomAudio()
        {
            return _clips[Random.Range(0, _clips.Count)];
        }
    }
}