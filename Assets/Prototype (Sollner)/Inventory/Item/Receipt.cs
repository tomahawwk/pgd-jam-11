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
        
        public bool CheckCraft(Item item1, Item item2)
        {
            int craftCache = _item1.GetHashCode() ^ _item2.GetHashCode();
            int recipeCache = item2.GetHashCode() ^ item1.GetHashCode();

            return craftCache == recipeCache;
        }

        public Item GetOutput() => _outPut;
    }
}