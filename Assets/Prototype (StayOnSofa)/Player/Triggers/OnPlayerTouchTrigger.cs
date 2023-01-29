using Prototype.PlayerPhysics;
using UnityEngine;

namespace Prototype.Triggers
{
    [RequireComponent(typeof(BoxCollider))]
    public abstract class OnPlayerTouchTrigger : MonoBehaviour
    {
        private BoxCollider _boxCollider => GetComponent<BoxCollider>();
        private void OnValidate() => _boxCollider.isTrigger = true;

        public abstract void OnPlayerTouch();
        
        private void OnTriggerEnter(Collider other)
        {
            var touchObject = other.gameObject;
            
            if (touchObject.TryGetComponent(out PlayerBody _))
            {
                OnPlayerTouch();
                Destroy(gameObject);
            }
        }
    }
}