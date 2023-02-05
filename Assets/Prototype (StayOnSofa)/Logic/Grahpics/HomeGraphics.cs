using System;
using UnityEngine;

namespace Prototype.Home
{

    [RequireComponent(typeof(Animator))]
    public class HomeGraphics : MonoBehaviour
    {
        private Animator _animator;

        [SerializeField] private float _speed = 1f;
        [SerializeField] private float _toLerp;
        [SerializeField] private float _fromLerp;
        
        private float _lerp;
        private float _lerpTarget;

        public void SetRotate(float angle)
        {
            var localEuler = transform.localEulerAngles;
            localEuler.y = angle;

            transform.localEulerAngles = localEuler;
        }
        
        public void AnimationToDown()
        {
            _lerpTarget = _fromLerp;
            _animator.Play("ToFace");
        }

        public void HardStop()
        {
            _animator.Play("LockSeat");
            _lerpTarget = _fromLerp;
            _lerp = _fromLerp;
        }

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _lerp = _fromLerp;
            _lerpTarget = _toLerp;
        }

        private void Update()
        {
            _lerp = Mathf.Lerp(_lerp, _lerpTarget, _speed * Time.deltaTime);
            SetRotate(_lerp);
        }
    }
}
