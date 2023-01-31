using System;
using System.Collections.Generic;
using Dialogue;
using Prototype__Sollner_.Inventory;
using UnityEngine;

public class InventorySystem : MonoSingleton<InventorySystem>
{
    public const int MaxStackItems = 4;
    public const string ReceiptPath = "Defaults/ReceiptsContainer";
    
    [SerializeField] private ReceiptContainer _receiptcontainer;
    
    public Action OnItemsChange;
    
    private List<Item> _item;
    public IEnumerable<Item> Items() => _item;

    private void Awake()
    {
        _item = new();
        
        if (_receiptcontainer == null)
            _receiptcontainer = Resources.Load<ReceiptContainer>(ReceiptPath);
    }

    public bool TryAddItem(Item item)
    {
        if (_item.Count < MaxStackItems)
        {
            _item.Add(item);
            OnItemsChange?.Invoke();
            
            return true;
        }

        return false;
    }

    public bool HasItem(Item item) => _item.Contains(item);

    public void TryCraft(Item item1, Item item2)
    {
        if (!_item.Contains(item1) || !_item.Contains(item2)) return;
        
        bool craftable = false;
        Receipt outReceipt = null;
        
        foreach (var receipt in _receiptcontainer.GetReceipts())
        {
            if (receipt.CheckCraft(item1, item2))
            {
                craftable = true;
                outReceipt = receipt;
                break;
            }
        }

        if (craftable)
        {
            int indexOfItem2 = _item.IndexOf(item2);
            _item[indexOfItem2] = outReceipt.GetOutput();
            
            RemoveItem(item1);
        }
    }

    public void RemoveItem(Item item)
    {
        if (_item.Contains(item))
            _item.Remove(item);
        
        OnItemsChange?.Invoke();
    }
}
