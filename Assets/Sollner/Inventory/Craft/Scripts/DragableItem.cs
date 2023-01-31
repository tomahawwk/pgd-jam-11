using System.Collections;
using System.Collections.Generic;
using Prototype;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragableItem : MonoBehaviour, IDragHandler, IEndDragHandler
{
    [SerializeField] private CraftingRecipes receipts;

    [SerializeField] Inventory _inventory;
    
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        receipts.Craft(_inventory);
    }
}
