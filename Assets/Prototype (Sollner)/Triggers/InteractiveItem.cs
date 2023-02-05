using System;
using UnityEngine;
using Dialogue;
using SaveState;
using UnityEngine.Events;

namespace Prototype
{
    public class InteractiveItem : Interactive
    {
        [SerializeField] public UnityEvent OnPickUp;
        public Action<bool> OnGraphicsState;
        
        private DialogueSystem _dialogue => DialogueSystem.Instance;
        private InventorySystem _inventory => InventorySystem.Instance;
        private SaveStateSystem _saveState => SaveStateSystem.Instance;
        

        [SerializeField] private Item _item;
  
        [SerializeField] private string _positive;
        [SerializeField] private string _negative;

        [SerializeField] [TextArea] private string _cantHoldMoreItems;

        private void Start()
        {
            UpdateGraphics();
        }

        public void UpdateGraphics()
        {
            OnGraphicsState?.Invoke(true);
            
            if (_item == null)
                return;

            if (_saveState.GetState(_item.GetHashCode()))
            {
                gameObject.SetActive(false);
                OnGraphicsState?.Invoke(false);
            }
        }

        public override void Interact()
        {
            if (_item == null) return;

            _dialogue.DialogueDoubleQuestion(_item.Icon, _item.Title, _item.Property, _positive, _negative, result =>
            {
                if (result)
                {
                    if (!_inventory.TryAddItem(_item))
                    {
                        _dialogue.Dialogue(_cantHoldMoreItems);
                        return;
                    }
                    
                    OnPickUp?.Invoke();
                    _saveState.SaveState(_item.GetHashCode(), true);
                    Destroy(gameObject);
                }
            });
        }
    }
}