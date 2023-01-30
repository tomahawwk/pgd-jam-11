using UnityEngine;
using Dialogue;

namespace Prototype
{
    public class InteractiveItem : Interactive
    {
        private DialogueSystem _dialogue => DialogueSystem.Instance;
        
        [SerializeField] private string _itemName;
        [SerializeField] [TextArea] private string _itemDescription;

        [SerializeField] private string _positive;
        [SerializeField] private string _negative;

        [SerializeField] private InventoryWindow inventoryWindow;
        [SerializeField] private Inventory targetInventory;
        [SerializeField] private Item item;

        public override void Interact()
        {
            _dialogue.DialogueDoubleQuestion(_itemName, _itemDescription, _positive, _negative, result =>
            {
                if (result)
                {
                    targetInventory.AddItem(item);
                    inventoryWindow.Redraw();

                    Destroy(gameObject);
                }
            });
        }
    }
}