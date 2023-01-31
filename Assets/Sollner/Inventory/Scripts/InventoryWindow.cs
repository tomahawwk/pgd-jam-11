using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Prototype;
using UnityEngine;
using UnityEngine.UI;


public class InventoryWindow : MonoBehaviour
{
    [SerializeField] private Inventory targetInventory;
    public RectTransform itemsPanel;

    readonly List<GameObject> drawnIcons = new List<GameObject>();

    private void Start()
    {
        Redraw();
    }

    private void OnItemAdded(Item item) => Redraw();

    public void Redraw()
    {
        Clear();
        for (var i = 0; i < targetInventory.inventoryItems.Count; i++)
        {
            var item = targetInventory.inventoryItems[i];
            var icon = new GameObject("Icon");
            icon.AddComponent<Image>().sprite = item.itemIcon;
            icon.AddComponent<DragableItem>();
            icon.transform.SetParent(itemsPanel);
        }
    }

    public void Clear()
    {
        for (var i = 0; i < drawnIcons.Count; i++)
        {
            Destroy(drawnIcons[i]);
        }
        drawnIcons.Clear();
    }
}
