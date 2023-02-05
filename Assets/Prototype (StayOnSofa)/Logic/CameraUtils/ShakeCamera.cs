using System.Collections;
using UnityEngine;

namespace Prototype.Logic.CameraUtils
{
    [RequireComponent(typeof(Camera))]
    public class ShakeCamera : MonoBehaviour
    {
        private bool _isPlaying;
        
        [SerializeField] private float _speed = 1f;
        [SerializeField] private float _amplitude = 0.1f;
        [SerializeField] private float _cameraMultiply = 10f;
        
        public void Play()
        {
            if (!_isPlaying)
                StartCoroutine(CoroutinePlay());
        }

        private Vector3 _cameraPrevLocation;
        
        public IEnumerator CoroutinePlay()
        {
            _cameraPrevLocation = transform.localPosition;
            
            float lerp = 0f;
            while (lerp < 1f)
            {
                float cameraShake = lerp * _cameraMultiply;

                float x = Mathf.Cos(cameraShake) * _amplitude;
                float y = Mathf.Sin(cameraShake) * _amplitude;
                float z = x + y;
                
                
                transform.localPosition = _cameraPrevLocation + new Vector3(x, y, z);
                lerp += Time.deltaTime * _speed;
                yield return null;
            }

            transform.localPosition = _cameraPrevLocation;
            _isPlaying = false;
        }
    }
}