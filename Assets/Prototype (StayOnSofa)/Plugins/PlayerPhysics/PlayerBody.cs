using UnityEngine;

namespace Prototype.PlayerPhysics
{
    [RequireComponent(typeof(CharacterController), typeof(Rigidbody))]
    public class PlayerBody : MonoBehaviour
    {
        public Vector3 ScaledGravity
        {
            get
            {
                return Physics.gravity * _gravityScale;
            }
        }

        [field:SerializeField] public LayerMask PlatformLayer { private set; get;}

        [SerializeField] private float _walkSpeed = 6f;
        [SerializeField] private float _groundedAcceleration = 8f;
        [SerializeField] private float _midAirSpeed = 6f;
        [SerializeField] private float _midAirAcceleration = 2f;
        [SerializeField] private float _gravityScale = 2.8f;
        
        private Rigidbody _rigidbody => GetComponent<Rigidbody>();
        private CharacterController _character;

        public Vector3 Center => transform.position + _character.center;
        public Vector3 WalkDirection { private set; get;}
        
        private Vector3 _velocityToApply;

        private Vector3 _groundNormalSurface;
        
        public Vector3 CalculateGroundNormal()
        {
            return _groundNormalSurface;
        }

        private Vector3 SlopedDirection(Vector3 direction, Vector3 slopeNormal)
        {
            Vector3 desiredDirection = Vector3.Cross(direction, slopeNormal);
            return Vector3.Cross(slopeNormal, desiredDirection);
        }
        
        public float GroundAngle => Vector3.Angle(CalculateGroundNormal(), Vector3.up);

        private void OnValidate()
        {
            _rigidbody.hideFlags = HideFlags.HideInInspector;
        }

        private enum MoveStates
        {
            Slope,
            Walk,
            Air,
        }
        
        private MoveStates _moveStates = MoveStates.Walk;
        
        private void Awake()
        {
            _character = GetComponent<CharacterController>();
            
            _rigidbody.useGravity = false;
            _rigidbody.isKinematic = true;
        }

        public void Move(Vector3 direction)
        {
            WalkDirection = direction;
        }
        
        private void FixedUpdate()
        {
            UpdatePhysics();
        }

        private void UpdatePhysics()
        {
            Vector3 normal = Vector3.up;
            RaycastHit hit;
            
            if (Physics.SphereCast(Center, _character.radius, Vector3.down, out hit, Mathf.Infinity, PlatformLayer))
                normal = hit.normal;

            _groundNormalSurface = normal;
        }

        private void Update()
        {
            var dt = Time.deltaTime;
            _character.Move(_velocityToApply * dt);

            UpdateMovementState();
            PhysicsUpdate(dt);
        }
        
        private void OnWalk(float dt)
        {
            Vector3 normal = CalculateGroundNormal();
            Vector3 velocity = _character.velocity;
	        
            Vector3 normalizedInputDirection = Vector3.Normalize(WalkDirection);
	        
            Vector3 desiredDirection = Vector3.Cross(normalizedInputDirection, normal);
            desiredDirection = Vector3.Cross(normal, desiredDirection);
            
            Vector3 velocityToAdd = desiredDirection * _walkSpeed - _velocityToApply;

            float currentAcceleration = _groundedAcceleration;
            float accelerationDot = 2f - (Vector3.Dot(velocity.normalized, normalizedInputDirection) + 1f);

            currentAcceleration += accelerationDot * _midAirAcceleration;
            
            _velocityToApply += velocityToAdd * dt * currentAcceleration;
        }
        
        private void OnAir(float dt)
        {
            Vector3 normalizedInputDirection = Vector3.Normalize(WalkDirection);
            float currentSpeed = Vector3.Dot(_velocityToApply, normalizedInputDirection);
	        
            float speedToAdd = _midAirSpeed - currentSpeed;
            float currentAcceleration = _midAirAcceleration;

            Vector3 velocity = _character.velocity;
            velocity.y = 0;
	        
            float accelerationDot = 2f - (Vector3.Dot(velocity.normalized, normalizedInputDirection) + 1f);

            currentAcceleration += accelerationDot * _midAirAcceleration;
            speedToAdd = Mathf.Max(Mathf.Min(speedToAdd, currentAcceleration * dt), 0f);
            
          
            _velocityToApply += normalizedInputDirection * speedToAdd;
            _velocityToApply += ScaledGravity * dt;
        }
        
        private void OnSlope(float dt)
        {
            Vector3 normal = CalculateGroundNormal();
            Vector3 normalizedInputDirection = Vector3.Normalize(WalkDirection);
	        
            Vector3 desiredDirection = Vector3.Cross(normalizedInputDirection, normal);
            desiredDirection = Vector3.Cross(normal, desiredDirection);
            Vector3 desiredVelocity = desiredDirection * _walkSpeed;
            if (desiredVelocity.y > 0f)
            {
                desiredVelocity.y = 0f;
            }
            desiredVelocity += SlopedDirection(Vector3.down, normal) * ScaledGravity.magnitude;
            Vector3 velocityToAdd = desiredVelocity - _velocityToApply;
                
            float currentAcceleration = _midAirAcceleration * 0.5f;
            _velocityToApply += velocityToAdd * dt * currentAcceleration;
        }
        
        private void PhysicsUpdate(float dt)
        {
            switch (_moveStates)
            {
                case MoveStates.Walk:
                    OnWalk(dt); 
                    break;
                case MoveStates.Slope:
                    OnSlope(dt); 
                    break;
                case MoveStates.Air:
                    OnAir(dt); 
                    break;
            }
        }
        
        private void UpdateMovementState()
        {
            MoveStates state = MoveStates.Air;
	        
            if (_character.isGrounded && GroundAngle <= 89f)
            {
                if (GroundAngle > _character.slopeLimit)
                {
                    if (_character.velocity.y <= 0f)
                    {
                        state = MoveStates.Slope;
                    }
                }
                else
                {
                    state = MoveStates.Walk;
                }
            }

            if (state != _moveStates)
            {
                _moveStates = state;
                ChangeMoveState(state);
            }
        }
        
        private void ChangeMoveState(MoveStates state)
        {
            if (state == MoveStates.Slope)
                _velocityToApply = _character.velocity;
        }
    }
}