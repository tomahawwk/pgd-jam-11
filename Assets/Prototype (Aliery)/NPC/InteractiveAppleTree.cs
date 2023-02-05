using System;
using Dialogue;
using Prototype.Logic.Interactions;
using Prototype.Plugins.FadeOutSystem;
using SaveState;
using UnityEngine;

namespace Prototype.Logic
{
    [RequireComponent(typeof(AudioSource))]
    public class InteractiveAppleTree : Interactive
    {
        private const string BunTitle = "Баюн";
        private const string YagaTitle = "Яга";

        private string SaveBreakKey = nameof(InteractiveAppleTree) + "Quest";
        private string SaveHasItem = nameof(InteractiveAppleTree) + "Has Quest Item";

        [SerializeField] private GameObject _haveStair;
        [SerializeField] private GameObject _haveApple;
        private AudioSource _audio => GetComponent<AudioSource>();
        private DialogueSystem _dialogueSystem => DialogueSystem.Instance;
        private SaveStateSystem _saveStateSystem => SaveStateSystem.Instance;
        private InventorySystem _inventorySystem => InventorySystem.Instance;
        private FadeOutSystem _fade => FadeOutSystem.Instance;

        [SerializeField] private Sprite _bunAvatar;
        [SerializeField] private Sprite _agaAvatar;

        [SerializeField] private Item _apple;
        [SerializeField] private Item _stair;
        private void SayBun(string text) => _dialogueSystem.DialogueAvatar(BunTitle, text, _bunAvatar);
        private void AgaSay(string text) => _dialogueSystem.DialogueAvatar(YagaTitle, text, _agaAvatar);

        private void SayBun(string text, string question1, string question2, Action<bool> result)
        {
            _dialogueSystem.DialogueDoubleQuestion(_bunAvatar, BunTitle, text, question1, question2, result);
        }

        private void SayAga(string text, string question1, string question2, Action<bool> result)
        {
            _dialogueSystem.DialogueDoubleQuestion(_agaAvatar, YagaTitle, text, question1, question2, result);
        }

        private void Start()
        {
            CheckStateBake();
        }

        private void CheckStateBake()
        {
            if (_saveStateSystem.GetState(SaveBreakKey))
            {
                _haveStair.SetActive(true);
                _haveApple.SetActive(false);
            }
        }

        public override void Interact()
        {
            if (!_saveStateSystem.GetState(SaveBreakKey))
            {

                if (!_inventorySystem.HasItem(_stair))
                {
                    AgaSay("М-м-м, какое яблочко! Хоть срывай и трави им дочурку.");
                }
                else
                {
                    SayBun("Подняться и сорвать, чоль, яблочко?", "Да!",
                        "Потом", result =>
                        {
                            if (result)
                            {
                                SceneRemover.RemoveCurrentScene();
                                _saveStateSystem.SaveState(SaveBreakKey, true);

                                _fade.FadeOut(() =>
                                {
                                    _inventorySystem.RemoveItem(_stair);
                                    _inventorySystem.TryAddItem(_apple);
                                    _audio.Play();

                                    CheckStateBake();
                                });
                            }
                        });
                }
            }
        }
    }
}
