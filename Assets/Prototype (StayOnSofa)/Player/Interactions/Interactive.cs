using System;
using System.Collections.Generic;
using UnityEngine;

namespace Prototype
{
    [RequireComponent(typeof(Collider))]
    public abstract class Interactive : MonoBehaviour
    {
        private Collider _collider => GetComponent<Collider>();
        private void OnValidate() => _collider.isTrigger = true;

        private List<Player> _savedPlayers;

        private void Awake()
        {
            _savedPlayers = new();
        }

        public abstract void Interact();

        private void OnTriggerEnter(Collider other)
        {
            var touchObject = other.gameObject;
            
            if (touchObject.TryGetComponent(out Player player))
            {
                if (!_savedPlayers.Contains(player))
                    _savedPlayers.Add(player);
                    
                player.AddInteractive(this);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            var touchObject = other.gameObject;

            if (touchObject.TryGetComponent(out Player player))
            {
                player.RemoveInteractive(this);
                
                if (_savedPlayers.Contains(player))
                    _savedPlayers.Add(player);
            }
        }

        private void OnDestroy()
        {
            foreach (var player in _savedPlayers)
                player.RemoveInteractive(this);
        }
    }
}