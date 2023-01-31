using System.Collections.Generic;
using UnityEngine;

namespace Prototype__Sollner_.Inventory
{
    [CreateAssetMenu(fileName = "ReceiptsContainer", menuName = "Items/ReceiptsContainer")]
    public class ReceiptContainer : ScriptableObject
    {
        [SerializeField] private List<Receipt> _receipts;
        public IEnumerable<Receipt> GetReceipts() => _receipts;
    }
}