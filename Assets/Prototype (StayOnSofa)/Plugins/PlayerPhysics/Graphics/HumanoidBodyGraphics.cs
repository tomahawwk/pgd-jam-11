using UnityEngine;

namespace Prototype.PlayerPhysics.Graphics
{
    [RequireComponent(typeof(Animator))]
    public class HumanoidBodyGraphics : MonoBehaviour
    {
        private static readonly int IdleRunProperty = Animator.StringToHash("Idle-Run");
        
        [SerializeField] private PlayerBody _body;
        [SerializeField] private float _rotateSpeed = 1f;
        [SerializeField] private float _lerpSpeed = 1f;
        
        private float _lerp;
        private Animator _animator;

        public bool BodyIsMove()
        {
            return Mathf.Abs(_body.WalkDirection.magnitude) > Mathf.Epsilon;
        }

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            float dt = Time.deltaTime;

            if (BodyIsMove())
                _lerp += dt * _lerpSpeed;
            else
                _lerp -= dt * _lerpSpeed;

            if (BodyIsMove())
            {
                Quaternion targetRotation = Quaternion.LookRotation(_body.WalkDirection);
                
                transform.rotation = Quaternion.Lerp( 
                    transform.rotation, targetRotation, _rotateSpeed * dt);
            }
            
            _lerp = Mathf.Clamp(_lerp, 0f, 1f);
            _animator.SetFloat(IdleRunProperty, _lerp);
        }
    }
}