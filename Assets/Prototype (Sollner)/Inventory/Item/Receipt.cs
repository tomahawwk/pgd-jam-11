using System.Linq;
using UnityEngine;

namespace Prototype__Sollner_.Inventory
{
    [CreateAssetMenu(fileName = "Receipt", menuName = "Items/Receipt")]
    public class Receipt : ScriptableObject
    {
        [SerializeField] private Item _item1;
        [SerializeField] private Item _item2;

        [Space]
        [SerializeField] private Item _outPut;

        public bool CheckCraft(Item item1, Item item2)
        {
            Item[] requiredItems =
            {
                _item1,
                _item2,
            };

            return requiredItems.Contains(item1) && requiredItems.Contains(item2);
        }

        public Item GetOutput() => _outPut;
    }
}