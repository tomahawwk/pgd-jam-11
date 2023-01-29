using UnityEngine;

namespace Prototype
{
    [RequireComponent(typeof(Collider))]
    public abstract class Interactive : MonoBehaviour
    {
        private Collider _collider => GetComponent<Collider>();
        private void OnValidate() => _collider.isTrigger = true;

        public abstract void Interact();

        private void OnTriggerEnter(Collider other)
        {
            var touchObject = other.gameObject;
            
            if (touchObject.TryGetComponent(out Player player))
            {
                player.AddInteractive(this);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            var touchObject = other.gameObject;
            
            if (touchObject.TryGetComponent(out Player player))
                player.RemoveInteractive(this);
        }
    }
}