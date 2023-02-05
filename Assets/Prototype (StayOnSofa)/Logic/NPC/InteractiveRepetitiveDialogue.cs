using Dialogue;
using UnityEngine;

namespace Prototype.Logic {
   public class InteractiveRepetitiveDialogue : Interactive
   {
      private DialogueSystem _dialogueSystem => DialogueSystem.Instance;
      [SerializeField] [TextArea] private string _text;
      public override void Interact()
      {
         _dialogueSystem.Dialogue(_text);
      }
   }
}
