using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InventoryWindow : MonoBehaviour
{
    [SerializeField] private Inventory targetInventory;
    public RectTransform itemsPanel;

    private void Start()
    {
        Redraw();
    }

    public void Redraw()
    {
        for (var i = 0; i < targetInventory.inventoryItems.Count; i++)
        {
            var item = targetInventory.inventoryItems[i];
            var icon = new GameObject("Icon");
            icon.AddComponent<Image>().sprite = item.itemIcon;
            icon.transform.parent = itemsPanel.transform;
        }
    }
}
