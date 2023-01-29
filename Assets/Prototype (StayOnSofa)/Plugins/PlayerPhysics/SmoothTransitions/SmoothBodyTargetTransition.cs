using Prototype.PlayerPhysics;
using UnityEngine;

namespace Prototype.Plugins.SmoothTransitions
{
    public class SmoothBodyTargetTransition : MonoBehaviour
    {
        [SerializeField] private Transform _flyTarget;
        [SerializeField] private PlayerBody _body;
        [SerializeField] private float _directionMultiplayer = 2f;
        
        [SerializeField] private float _cameraSpeed;
        private void Update()
        {
            float dt = Time.deltaTime;

            Vector3 targetPosition = _flyTarget.position + _body.WalkDirection * _directionMultiplayer;
            transform.position = Vector3.Lerp(transform.position, targetPosition, _cameraSpeed + dt);
        }
    }
}