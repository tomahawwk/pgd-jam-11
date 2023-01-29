using Dialogue;
using Prototype.PlayerPhysics;
using UnityEngine;

namespace Prototype
{

    [RequireComponent(typeof(PlayerBody), typeof(Player))]
    public class InputPlayer : MonoBehaviour
    {
        private DialogueSystem _dialogue => DialogueSystem.Instance;
        
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

        private void Update()
        {
            Vector3 direction = Vector3.zero;

            if (Input.GetKey(Forward))
                direction.z = 1;
            if (Input.GetKey(Back))
                direction.z = -1;
            if (Input.GetKey(StrafeLeft))
                direction.x = -1;
            if (Input.GetKey(StrafeRight))
                direction.x = 1;

            if (_dialogue.IsOpen())
                direction = Vector3.zero;

            _character.Move(direction);

            if (!_dialogue.IsOpen())
            {
                if (Input.GetKeyDown(Interact))
                    _player.Interact();
            }
        }
    }
}