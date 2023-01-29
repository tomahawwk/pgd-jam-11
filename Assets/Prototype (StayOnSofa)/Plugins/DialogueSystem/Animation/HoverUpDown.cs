using System;
using UnityEngine;

namespace Dialogue
{
    [RequireComponent(typeof(RectTransform))]
    public class HoverUpDown : MonoBehaviour
    {
        private RectTransform _rectTransform;
        
        [SerializeField] private float _radius = 2f;
        [SerializeField] private float _speed = 5f;
        
        private Vector3 _startPosition;
        private float _hover;

        private void Awake() => _rectTransform = GetComponent<RectTransform>();
        private void Start() => _startPosition = _rectTransform.anchoredPosition;
        
        private void Update()
        {
            _hover += Time.deltaTime * _speed;

            Vector3 position = _rectTransform.anchoredPosition;
            position.y = _startPosition.y + Mathf.Cos(_hover) * _radius;

            _rectTransform.anchoredPosition = position;
        }
    }
}