using UnityEngine;
using Dialogue;

namespace Prototype
{
    public class InteractiveItem : Interactive
    {
        private DialogueSystem _dialogue => DialogueSystem.Instance;
        private InventorySystem _inventory => InventorySystem.Instance;

        [SerializeField] private Item _item;
  
        [SerializeField] private string _positive;
        [SerializeField] private string _negative;

        [SerializeField] [TextArea] private string _cantHoldMoreItems;
        
        public override void Interact()
        {
            _dialogue.DialogueDoubleQuestion(_item.Icon, _item.Title, _item.Property, _positive, _negative, result =>
            {
                if (result)
                {
                    if (!_inventory.TryAddItem(_item))
                        _dialogue.Dialogue(_cantHoldMoreItems);
                    else 
                        Destroy(gameObject);
                }
            });
        }
    }
}