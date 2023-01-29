using System;
using Dialogue;
using Prototype.PlayerPhysics;
using Prototype.PlayerPhysics.Utils;
using UnityEngine;

namespace Prototype
{

    [RequireComponent(typeof(PlayerBody), typeof(Player))]
    public class InputPlayer : MonoBehaviour
    {
        private DialogueSystem _dialogue => DialogueSystem.Instance;

        [SerializeField] private Transform _cameraTarget;
        
        public KeyCode Interact = KeyCode.E;
        public KeyCode Forward = KeyCode.W;
        public KeyCode Back = KeyCode.S;
        public KeyCode StrafeLeft = KeyCode.A;
        public KeyCode StrafeRight = KeyCode.D;
        
        private PlayerBody _character;
        private Player _player;

        private void Awake()
        {
            _character = GetComponent<PlayerBody>();
            _player = GetComponent<Player>();
        }

        private Vector3 _drawDebugView;

        private void OnDrawGizmos()
        {
            if (_cameraTarget == null) return;
            BoxGizmos.Arrow(Color.green, transform.position, _drawDebugView);
        }

        private void Update()
        {
            var viewDirForward = Vector3.Normalize(transform.position - _cameraTarget.position);
            viewDirForward.y = 0;
            
            var viewDirRight = Vector3.Cross(viewDirForward, Vector3.down);
            
            Vector3 forward = Vector3.zero;
            Vector3 right = Vector3.zero;

            if (Input.GetKey(Forward))
                forward = viewDirForward;
            if (Input.GetKey(Back))
                forward = -viewDirForward;
            if (Input.GetKey(StrafeLeft))
                right = -viewDirRight;
            if (Input.GetKey(StrafeRight))
                right = viewDirRight;

            Vector3 direction = Vector3.Normalize(forward + right);
            
            if (_dialogue.IsOpen())
                direction = Vector3.zero;

            _character.Move(direction);

            _drawDebugView = direction;
            
            if (!_dialogue.IsOpen())
            {
                if (Input.GetKeyDown(Interact))
                    _player.Interact();
            }
        }
    }
}