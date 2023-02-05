using System;
using Dialogue;
using Prototype.Plugins.FadeOutSystem;
using SaveState;
using UnityEngine;

namespace Prototype.Logic
{
    [RequireComponent(typeof(AudioSource))]
    public class InteractiveBake : Interactive
    {
        private const string BunTitle = "Баюн";
        private const string YagaTitle = "Яга";
        
        private string SaveBreakKey = nameof(InteractiveBake) + "Quest";
        private string SaveHasItem = nameof(InteractiveBake) + "Has Quest Item";

        [SerializeField] private GameObject _openBake;
        [SerializeField] private GameObject _closeBake;
        [SerializeField] private GameObject _bakeItem;

        private AudioSource _audio => GetComponent<AudioSource>();
        private DialogueSystem _dialogueSystem => DialogueSystem.Instance;
        private SaveStateSystem _saveStateSystem => SaveStateSystem.Instance;
        private InventorySystem _inventorySystem => InventorySystem.Instance;
        private FadeOutSystem _fade => FadeOutSystem.Instance;

        [SerializeField] private Sprite _bunAvatar;
        [SerializeField] private Sprite _agaAvatar;

        [SerializeField] private Item _axe;
        [SerializeField] private Item _give;

        private void SayBun(string text) =>  _dialogueSystem.DialogueAvatar(BunTitle, text, _bunAvatar);
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
                _openBake.SetActive(true);
                _closeBake.SetActive(false);
            }

            if (_saveStateSystem.GetState(SaveHasItem))
            {
                _bakeItem.SetActive(false);
            }
        }
        
        public override void Interact()
        {
            if (!_saveStateSystem.GetState(SaveBreakKey))
            {

                if (!_inventorySystem.HasItem(_axe))
                {
                    AgaSay("Глянь-ка, намертво пристала! Чем бы сломать эту заслонку…");
                    SayBun("Давай в избушке по чему-нибудь долбанем, авось она  слушаться снова начнет!");
                }
                else
                {
                    SayBun("Топором руби, бабка, топором! Никаких полумер!", "Эх, сено-солома, была — не была!",
                        "Потом", result =>
                        {
                            if (result)
                            {
                                _saveStateSystem.SaveState(SaveBreakKey, true);
                                
                                _fade.FadeOut(() =>
                                {
                                    _inventorySystem.RemoveItem(_axe);
                                    _audio.Play();
                                    
                                    CheckStateBake();
                                });
                            }
                        });
                }
            }
            else
            {
                if (!_saveStateSystem.GetState(SaveHasItem))
                {
                    SayAga("Здесь что-то лежит", "Поднять",
                        "Потом", result =>
                        {
                            if (result)
                            {
                                if (_inventorySystem.TryAddItem(_give))
                                {
                                    _dialogueSystem.Dialogue("(получено серебряное блюдечко)");
                                    _saveStateSystem.SaveState(SaveHasItem, true);
                                    CheckStateBake();
                                }
                                else
                                {
                                    AgaSay("...");
                                }
                            }
                        });
                }
                else
                {
                    AgaSay("...");
                }
            }
        }
    }
}
