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
        
        public virtual void OnTriggerEnter(Collider other)
        {
            var touchObject = other.gameObject;
            
            if (touchObject.TryGetComponent(out Player _))
            {
                OnPlayerTouch();
                Destroy();
            }
        }

        public virtual void Destroy()
        {
            Destroy(gameObject);
        }
    }
}