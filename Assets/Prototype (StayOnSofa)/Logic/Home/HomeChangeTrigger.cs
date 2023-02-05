using Prototype.Home.Details;
using Prototype.Logic.CameraUtils;
using Prototype.Triggers;
using UnityEngine;

namespace Prototype.Home
{
    public class HomeChangeTrigger : OnPlayerTouchTrigger
    {
        [SerializeField] private ShakeCamera _camera;
        [SerializeField] private LightsRotate _lights;
        public override void OnPlayerTouch()
        {
            _camera.Play();
            _lights.Play();
        }

        public override void Destroy()
        {
            return;
        }
    }
}
