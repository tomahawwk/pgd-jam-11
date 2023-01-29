using System.Collections.Generic;
using Prototype.PlayerPhysics;
using UnityEngine;

namespace Prototype
{
    [RequireComponent(typeof(PlayerBody))]
    public class Player : MonoBehaviour
    {
        private List<Interactive> _interactives;

        public bool HasInteractions => _interactives.Count > 0;

        private void Awake()
        {
            _interactives = new List<Interactive>();
        }

        public void AddInteractive(Interactive item)
        {
            if (!_interactives.Contains(item))
                _interactives.Add(item);
        }

        public void Interact()
        {
            if (_interactives.Count > 0)
                _interactives[0].Interact();
        }

        public void RemoveInteractive(Interactive item)
        {
            if (_interactives.Contains(item))
                _interactives.Remove(item);
        }
    }
}