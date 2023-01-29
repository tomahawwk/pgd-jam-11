using Dialogue;
using UnityEngine;

namespace Prototype.Triggers.Types
{
    public class TriggerMessageBox : OnPlayerTouchTrigger
    {
        [TextArea][SerializeField] private string _text;
        private DialogueSystem _dialogueSystem => DialogueSystem.Instance;
        
        public override void OnPlayerTouch()
        {
            _dialogueSystem.Dialogue(_text);
        }
    }
}