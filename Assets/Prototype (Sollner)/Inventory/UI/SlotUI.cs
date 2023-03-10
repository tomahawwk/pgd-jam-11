using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Prototype__Sollner_.Inventory.UI
{
    [RequireComponent(typeof(RectTransform), typeof(BumpEffect))]
    public class SlotUI : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerEnterHandler, IPointerExitHandler
    {
        private RectTransform _rectTransform;
        [SerializeField] private Image _image;
        [SerializeField] private TextMeshProUGUI _property;
        [SerializeField] private TextMeshProUGUI _title;

        [SerializeField] private Color _onDragColor = new Color(0f, 0f, 0f, 0.28f);
        [SerializeField] private Color _unDragColor = Color.white;

        [SerializeField] private GameObject _propertyLayer;
        
        private void Awake() => _rectTransform = GetComponent<RectTransform>();
        private BumpEffect _bumpEffect => GetComponent<BumpEffect>();

        public Item Item { private set; get;}
        private InventoryUI _inventory;

        public void Bump() => _bumpEffect.Play();
        public void Create(Item item, InventoryUI ui)
        {
            Item = item;
            _inventory = ui;
            
            _image.sprite = null;

            if (item != null)
            {
                _image.sprite = item.Icon;
            }

            _image.gameObject.SetActive(item != null);
        }

        private bool _isDrag;
        private Vector3 _previousPosition;
        
        public void OnBeginDrag(PointerEventData eventData)
        {
            if (!_isDrag)
            {
                if (Item != null)
                {
                    Bump();

                    _image.color = _onDragColor;
                    _inventory.SetCursorVisible(true);
                    _inventory.SetCursorIcon(Item.Icon);
                    
                    _isDrag = true;
                    _previousPosition = _rectTransform.position;
                }
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (_isDrag)
            {
                _image.color = _unDragColor;
                _inventory.SetCursorVisible(false);
                
                _isDrag = false;
                _rectTransform.position = _previousPosition;

                var overTouch = UIHelper.GetPointerUI();
                if (overTouch != null)
                {
                    if (overTouch.TryGetComponent(out SlotUI otherSlot))
                    {
                        if (otherSlot.Item != null)
                        {
                            if (_inventory.TryCraft(Item, otherSlot.Item))
                                otherSlot.Bump();
                        }
                    }
                }
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (_isDrag)
                _inventory.SetCursor(Input.mousePosition);
        }

        private bool _propertyDrag;
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!_propertyDrag)
            {
                if (Item != null)
                {
                    _property.text = Item.Property;
                    _title.text = Item.Title;
                    
                    _propertyDrag = true;
                    
                    _propertyLayer.SetActive(true);
                }
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (_propertyDrag)
            {
                _propertyLayer.SetActive(false);
                _propertyDrag = false;
            }
        }
    }
}