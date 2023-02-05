using System;
using Dialogue;
using Prototype.Plugins.FadeOutSystem;
using SaveState;
using UnityEngine;

namespace Prototype.Logic
{
    [RequireComponent(typeof(AudioSource))]
    public class InteractiveFishpool : Interactive
    {
        private const string BunTitle = "Баюн";
        private const string YagaTitle = "Яга";

        private AudioSource _audio => GetComponent<AudioSource>();
        private DialogueSystem _dialogueSystem => DialogueSystem.Instance;
        private SaveStateSystem _saveStateSystem => SaveStateSystem.Instance;
        private InventorySystem _inventorySystem => InventorySystem.Instance;
        private FadeOutSystem _fade => FadeOutSystem.Instance;

        [SerializeField] private Sprite _bunAvatar;
        [SerializeField] private Sprite _agaAvatar;

        [SerializeField] private Item _harpun;
        [SerializeField] private Item _goldfish;

        private void SayBun(string text) => _dialogueSystem.DialogueAvatar(BunTitle, text, _bunAvatar);
        private void AgaSay(string text) => _dialogueSystem.DialogueAvatar(YagaTitle, text, _agaAvatar);

        private void SayBun(string text, string question1, string question2, Action<bool> result)
        {
            _dialogueSystem.DialogueDoubleQuestion(_bunAvatar, BunTitle, text, question1, question2, result);
        }

        private void AgaSay(string text, string question1, string question2, Action<bool> result)
        {
            _dialogueSystem.DialogueDoubleQuestion(_agaAvatar, YagaTitle, text, question1, question2, result);
        }

        public override void Interact()
        {


            if (!_inventorySystem.HasItem(_harpun))
            {
                AgaSay("Можно из этой лужи что-нибудь выловить — да было бы чем.");
            }
            else
            {
                AgaSay("Закинуть гарпун?", "Да!","Потом", result =>
                        {
                            if (result)
                            {

                                AgaSay("Ловись, рыбка, большая и маленькая!");

                                _fade.FadeOut(() =>
                                {
                                    _audio.Play();
                                });

                                AgaSay("А, мальки одни… Без труда… как там?", "Закинуть гарпун!", "Уйти", result =>
                                { 
                                    if (result)
                                    {
                                        AgaSay("Ловись, рыбка, большая и маленькая!");
                                        _fade.FadeOut(() =>
                                        {
                                            _audio.Play();
                                        });

                                        AgaSay("Тьфу, пока избушку себе не верну, всё придётся своими рученьками делать.", "Закинуть гарпун!", "Уйти", result =>
                                        {
                                            if (result)
                                            {
                                                AgaSay("Ловись, рыбка, большая и маленькая!");
                                                _fade.FadeOut(() =>
                                                {
                                                    _inventorySystem.RemoveItem(_harpun);
                                                    _inventorySystem.TryAddItem(_goldfish);
                                                    _audio.Play();
                                                });
                                            }
                                        });
                                                

                                    }
                                });
                            }
                         });

        }

                }
        
    }
}
