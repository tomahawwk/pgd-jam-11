using System.Collections.Generic;
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

        private List<Item> _craftBuffer;
        
        public bool CheckCraft(Item item1, Item item2)
        {
            _craftBuffer.Clear();
            
            _craftBuffer.Add(_item1);
            _craftBuffer.Add(_item2);

            if (_craftBuffer.Contains(item1))
                _craftBuffer.Remove(item1);
            
            if (_craftBuffer.Contains(item2))
                _craftBuffer.Remove(item2);

            return _craftBuffer.Count == 0;
        }

        public Item GetOutput() => _outPut;
    }
}