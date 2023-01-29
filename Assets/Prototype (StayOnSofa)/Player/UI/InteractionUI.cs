using Prototype.PlayerPhysics;
using UnityEngine;

namespace Prototype
{
    public class InteractionUI : MonoBehaviour
    {
        [SerializeField] private Vector3 _offset;

        [SerializeField] private Player _player;
        [SerializeField] private PlayerBody _body;
        [SerializeField] private RectTransform _interactionHelp;
        [SerializeField] private Camera _camera;

        private void Update()
        {
            var center = _body.Center;
            var uiSpace = _camera.WorldToScreenPoint(center + _offset);

            _interactionHelp.anchoredPosition = uiSpace;
            _interactionHelp.gameObject.SetActive(_player.HasInteractions);
        }
    }
}