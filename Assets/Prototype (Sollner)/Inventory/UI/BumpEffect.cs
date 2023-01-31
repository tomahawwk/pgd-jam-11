using System.Collections;
using UnityEngine;

namespace Prototype__Sollner_.Inventory.UI
{
    [RequireComponent(typeof(RectTransform))]
    public class BumpEffect : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _multiply;
        
        private RectTransform _rectTransform;
        private void Awake() => _rectTransform = GetComponent<RectTransform>();

        private bool _isPlaying;
        
        public void Play()
        {
            if (!_isPlaying)
            {
                StartCoroutine(RoutinePlay());
                _isPlaying = true;
            }
        }

        private Vector3 _scale;

        private IEnumerator RoutinePlay()
        {
            _scale = _rectTransform.localScale;
            
            float bump = 0;
            while (bump < 1f)
            {
                bump += Time.deltaTime * _speed;
                _rectTransform.localScale = _scale + _scale * _multiply * bump;
                yield return null;
            }

            bump = 1f;
            while (bump > 0)
            {
                bump -= Time.deltaTime * _speed;
                _rectTransform.localScale = _scale + _scale * _multiply * bump;
                yield return null;
            }

            _rectTransform.localScale = _scale;
            _isPlaying = false;
        }
    }
}