using Dialogue;
using Prototype.Plugins.FadeOutSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Prototype
{
    public class InteractiveSceneLoading : Interactive
    {
        private DialogueSystem _dialogue => DialogueSystem.Instance;
        private FadeOutSystem _fadeOut => FadeOutSystem.Instance;

        [SerializeField] private string _sceneToLoad;
        
        [SerializeField] private string _title;
        [SerializeField] [TextArea] private string _property;

        [SerializeField] private string _positive;
        [SerializeField] private string _negative;
        
        public override void Interact()
        {
            _dialogue.DialogueDoubleQuestion(_title, _property, _positive, _negative, result =>
            {
                if (result)
                    _fadeOut.FadeOut(() =>
                    {
                        SceneManager.LoadScene(_sceneToLoad);
                    });
            });
        }
    }
}