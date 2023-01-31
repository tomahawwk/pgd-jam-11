using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct ItemAmount
{
    public Item item;
    [Range(1, 1)] public int Amount;
}


[CreateAssetMenu(fileName = "Item Name", menuName = "PGD JAM/New Receipt")]
public class CraftingRecipes : ScriptableObject
{
    public List<ItemAmount> itemMaterials;
    public List<ItemAmount> itemResult;

    public void Craft(Inventory inventory)
    {
        foreach (ItemAmount itemAmount in itemMaterials)
        {
            for (int i = 0; i < itemAmount.Amount; i++)
            {
                inventory.RemoveItem(itemAmount.item);
            }
        }
        
        foreach (ItemAmount itemAmount in itemResult)
        {
            for (int i = 0; i < itemAmount.Amount; i++)
            {
                inventory.AddItem(itemAmount.item);
            }
        }
    }
}
