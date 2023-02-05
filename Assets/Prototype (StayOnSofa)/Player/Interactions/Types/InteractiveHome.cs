using System;
using Dialogue;
using Prototype.Home;
using Prototype.Plugins.FadeOutSystem;
using SaveState;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Prototype
{
    [RequireComponent(typeof(AudioSource))]
    public class InteractiveHome : Interactive
    {

        private AudioSource _source => GetComponent<AudioSource>();
        private string SaveState => "SaveState" + SceneManager.GetActiveScene().name;
        [SerializeField] private HomeGraphics _graphics;
        
        private DialogueSystem _dialogueSystem => DialogueSystem.Instance;
        private SaveStateSystem _saveStateSystem => SaveStateSystem.Instance;
        private InventorySystem _inventorySystem => InventorySystem.Instance;

        [SerializeField] private AudioClip _hatStanding;
        [SerializeField] private AudioClip _doorOpen;
        
        private FadeOutSystem _fadeOut => FadeOutSystem.Instance;

        [SerializeField] private string _sceneToLoad;
        
        [SerializeField] private string _title;
        [SerializeField] [TextArea] private string _property;

        [SerializeField] private string _positive;
        [SerializeField] private string _negative;
        
        private bool _isDown;

        private void Start()
        {
            if (_saveStateSystem.GetState(SaveState))
            {
                _graphics.HardStop();
                _isDown = true;
            }
            else
            {
                _source.clip = _hatStanding;
                _source.Play();
            }
        }

        public void HutIsDown()
        {
            _saveStateSystem.SaveState(SaveState, true);
            _isDown = true;
            
            _source.clip = _hatStanding;
            _source.Play();
            
            _graphics.AnimationToDown();
        }

        public override void Interact()
        {
            if (_isDown)
            {
                _dialogueSystem.DialogueDoubleQuestion(_title, _property, _positive, _negative, result =>
                {
                    if (result)
                    {
                        _source.clip = _doorOpen;
                        _source.Play();
                        _fadeOut.FadeOut(() => { SceneManager.LoadScene(_sceneToLoad); });
                    }
                });
            }
            else
            {
                _dialogueSystem.Dialogue("...");
            }
        }
    }
}