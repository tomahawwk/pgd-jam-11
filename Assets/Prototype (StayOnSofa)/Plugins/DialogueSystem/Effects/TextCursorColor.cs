using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Dialogue.Effects
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class TextCursorColor : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Color _default = Color.white;
        [SerializeField] private Color _selected = Color.yellow;
        
        private TextMeshProUGUI _textMeshProUGUI => GetComponent<TextMeshProUGUI>();

        private void Start()
        {
            _textMeshProUGUI.color = _default;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _textMeshProUGUI.color = _selected;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _textMeshProUGUI.color = _default;
        }
    }
}