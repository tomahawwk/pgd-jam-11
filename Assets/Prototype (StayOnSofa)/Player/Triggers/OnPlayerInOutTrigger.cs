using System;
using UnityEngine;
using UnityEngine.Events;

namespace Prototype.Triggers
{
    [RequireComponent(typeof(BoxCollider))]
    public class OnPlayerInOutTrigger : MonoBehaviour
    {
        [SerializeField] public UnityEvent _onPlayerIn;
        [SerializeField] public UnityEvent _onPlayerOut;

        private BoxCollider _boxCollider => GetComponent<BoxCollider>();
        private void OnValidate() => _boxCollider.isTrigger = true;
        
        public void OnTriggerEnter(Collider other)
        {
            var touchObject = other.gameObject;
            
            if (touchObject.TryGetComponent(out Player _))
                _onPlayerIn?.Invoke();
        }

        public void OnTriggerExit(Collider other)
        {
            var touchObject = other.gameObject;
            
            if (touchObject.TryGetComponent(out Player _))
                _onPlayerOut?.Invoke();
        }
    }
}