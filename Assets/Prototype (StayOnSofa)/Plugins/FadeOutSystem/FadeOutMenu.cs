using UnityEngine;
using UnityEngine.UI;

namespace Prototype.Plugins.FadeOutSystem
{
    public class FadeOutMenu : MonoBehaviour
    {
        [SerializeField] private Image _image;
        private void Awake() => DontDestroyOnLoad(gameObject);

        public bool SetLock(bool value) => _image.raycastTarget = value;
        
        public void SetAlpha(float alpha)
        {
            Color color = _image.color;
            color.a = alpha;

            _image.color = color;
        }
    }
}