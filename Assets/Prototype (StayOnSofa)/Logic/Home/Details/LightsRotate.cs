using System;
using System.Collections;
using UnityEngine;

namespace Prototype.Home.Details
{
    [RequireComponent(typeof(Light))]
    public class LightsRotate : MonoBehaviour
    {
        private bool _isPlaying;
        [SerializeField] private float _speed = 1f;
        [SerializeField] private int _blinkEvery = 4;
        
        private Light _light;
        
        private void Awake()
        {
            _light = GetComponent<Light>();
        }

        public void Play()
        {
            if (!_isPlaying)
            {
                _isPlaying = true;
                StartCoroutine(CoroutinePlay());
            }
        }

        public void Update()
        {
            if (Input.GetKey(KeyCode.G))
                Play();
        }

        private Vector3 _cameraPrevEuler;
        
        public IEnumerator CoroutinePlay()
        {
            int frame = 0;
            _cameraPrevEuler = transform.localEulerAngles;
            
            float lerp = 0f;
            while (lerp < 1f)
            {
                frame += 1;
                
                lerp += Time.deltaTime * _speed;
                float angle = 360 * lerp;

                var euler = _cameraPrevEuler;
                euler.y = _cameraPrevEuler.y + angle;

                bool enableLights = frame % _blinkEvery != 0;
                _light.enabled = enableLights;
                
                //transform.localEulerAngles = euler;
                yield return null;
            }

            _light.enabled = true;
            transform.localEulerAngles = _cameraPrevEuler;
            _isPlaying = false;
        }
    }
}