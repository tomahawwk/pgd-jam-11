using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Dialogue
{
    [RequireComponent(typeof(Button))]
    public class DialogueQuestion : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _title;
        private Button _button => GetComponent<Button>();

        public void SetText(string text)
        {
            _title.text = text;
        }

        public void Create(string text, Action onClick)
        {
            _title.text = $"{text}";
            
            _button.onClick.RemoveAllListeners();
            _button.onClick.AddListener(() =>
            {
                onClick?.Invoke();
                _button.onClick.RemoveAllListeners();
            });
        }
    }
}