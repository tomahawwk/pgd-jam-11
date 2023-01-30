using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
     public List<Item> inventoryItems = new List<Item>();

     private void Awake()
     {
          for (var i = 0; i < inventoryItems.Count; i++)
          {
               AddItem((inventoryItems[i]));
          }
     }

     public void AddItem(Item item)
     {
          inventoryItems.Add(item);
     }
}
