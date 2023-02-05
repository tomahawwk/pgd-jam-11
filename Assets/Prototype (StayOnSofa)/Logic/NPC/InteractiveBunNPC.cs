using System;
using Dialogue;
using SaveState;
using UnityEngine;

namespace Prototype.Logic
{
    public class InteractiveBunNPC : Interactive
    {
        private string InteractionKey = nameof(InteractiveBunNPC) + "StartQuestion";
        
        private const string BunTitle = "Колобок";
        private const string YagaTitle = "Яга";
        
        private DialogueSystem _dialogueSystem => DialogueSystem.Instance;
        private SaveStateSystem _saveStateSystem => SaveStateSystem.Instance;
        
        [SerializeField] private Sprite _bunAvatar;
        [SerializeField] private Sprite _agaAvatar;

        private void SayBun(string text) =>  _dialogueSystem.DialogueAvatar(BunTitle, text, _bunAvatar);
        private void AgaSay(string text) => _dialogueSystem.DialogueAvatar(YagaTitle, text, _agaAvatar);

        private void SayBun(string text, string question1, string question2, Action<bool> result)
        {
            _dialogueSystem.DialogueDoubleQuestion(_bunAvatar, BunTitle, text, question1, question2, result);
        }

        public override void Interact()
        {
            if (!_saveStateSystem.GetState(InteractionKey))
            {
                SayBun("Убей меня..", "Позже", "...", result =>
                {
                    SayBun("...");
                    AgaSay("...");
                });
                
                _saveStateSystem
                    .SaveState(InteractionKey, true);
            }
            else
            {
                SayBun("...");
                AgaSay("...");
            }
        }
    }
}