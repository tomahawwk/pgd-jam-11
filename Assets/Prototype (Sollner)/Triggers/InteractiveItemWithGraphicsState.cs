using UnityEngine;

namespace Prototype
{
    public class InteractiveItemWithGraphicsState : InteractiveItem
    {
        [SerializeField] private GameObject _graphics;
        
        public void OnEnable()
        {
            OnGraphicsState += GraphicsStateChanged;
        }

        public void OnDisable()
        {
            OnGraphicsState -= GraphicsStateChanged;
        }

        private void GraphicsStateChanged(bool value)
        {
            _graphics.SetActive(value);
        }
    }
}