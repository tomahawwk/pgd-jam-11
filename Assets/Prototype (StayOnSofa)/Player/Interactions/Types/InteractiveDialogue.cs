using Dialogue;
using UnityEngine;

namespace Prototype
{
    public class InteractiveDialogue : Interactive
    {
        [TextArea][SerializeField] private string _text;
        private DialogueSystem _dialogue => DialogueSystem.Instance;
        
        public override void Interact()
        {
            _dialogue.Dialogue(_text);
        }
    }
}