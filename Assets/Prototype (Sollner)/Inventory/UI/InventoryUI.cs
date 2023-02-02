using System;
using Prototype__Sollner_.Inventory.UI;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas))]
public class InventoryUI : MonoBehaviour
{
    private int Slots => InventorySystem.MaxStackItems;
    private InventorySystem _inventory => InventorySystem.Instance;
    
    [SerializeField] private Image _cursor;

    public void SetCursorVisible(bool value) => _cursor.gameObject.SetActive(value);
    public void SetCursor(Vector3 position) => _cursor.rectTransform.position = position;
    public void SetCursorIcon(Sprite sprite) => _cursor.sprite = sprite;

    [SerializeField] private SlotUI[] _slots;

    private void OnEnable() => _inventory.OnItemsChange += ReDraw;

    private void Awake()
    {
        SetCursorVisible(false);
    }

    private void OnDisable()
    {
        if (_inventory != null)
            _inventory.OnItemsChange -= ReDraw;
    }

    private void OnValidate()
    {
        if (_slots != null)
        {
            if (_slots.Length != Slots)
                _slots = new SlotUI[Slots];
        }
    }

    public void Start()
    {
        ReDraw();
    }

    private void Clear()
    {
        foreach (var slot in _slots)
            slot.Create(null, this);
    }

    public bool TryCraft(Item item1, Item item2)
    {
        return _inventory.TryCraft(item1, item2);
    }

    public void ReDraw()
    {
        Clear();

        int index = 0;
        
        foreach (var item in _inventory.Items())
        {
            if (index >= Slots) break;
            var slot = _slots[index];
            
            slot.Create(item, this);
            index += 1;
        }
    }
}
