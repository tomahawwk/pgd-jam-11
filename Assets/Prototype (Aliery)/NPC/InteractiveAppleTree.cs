using Dialogue;
using Prototype.Plugins.FadeOutSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Prototype
{
    public class InteractiveAppleTree : Interactive
    {
        private DialogueSystem _dialogue => DialogueSystem.Instance;
        private FadeOutSystem _fadeOut => FadeOutSystem.Instance;

        [SerializeField] private Sprite _fistPersona;
        [SerializeField] private Sprite _secondPersona;

        public override void Interact()
        {
            _dialogue.DialogueAvatar("Ѕаба яга", "ћ-м-м, какое €блочко! ’оть срывай и трави им дочурку.", _secondPersona);
        }
    }
}
