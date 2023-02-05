using Dialogue;
using SaveState;
using Prototype.Plugins.FadeOutSystem;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using Prototype.Logic.Interactions;

namespace Prototype
{
    [RequireComponent(typeof(AudioSource))]
    public class InteractiveVodyanoi : Interactive
    {
        private const string VodyanTitle = "Водяной";
        private const string YagaTitle = "Яга";

        private string SaveFirstDialogue = nameof(InteractiveVodyanoi) + "Quest";
        private string SaveHasItem = nameof(InteractiveVodyanoi) + "Has Quest Item";


        private AudioSource _audio => GetComponent<AudioSource>();
        private DialogueSystem _dialogueSystem => DialogueSystem.Instance;
        private SaveStateSystem _saveStateSystem => SaveStateSystem.Instance;
        private InventorySystem _inventorySystem => InventorySystem.Instance;
        private FadeOutSystem _fade => FadeOutSystem.Instance;

        [SerializeField] private Sprite _vodyanAvatar;
        [SerializeField] private Sprite _agaAvatar;

        [SerializeField] private Item _saucer;
        [SerializeField] private Item _harpun;

        private void SayVodyan(string text) => _dialogueSystem.DialogueAvatar(VodyanTitle, text, _vodyanAvatar);
        private void SayAga(string text) => _dialogueSystem.DialogueAvatar(YagaTitle, text, _agaAvatar);

        private void SayVodyan(string text, string question1, string question2, Action<bool> result)
        {
            _dialogueSystem.DialogueDoubleQuestion(_vodyanAvatar, VodyanTitle, text, question1, question2, result);
        }

        private void SayAga(string text, string question1, string question2, Action<bool> result)
        {
            _dialogueSystem.DialogueDoubleQuestion(_agaAvatar, YagaTitle, text, question1, question2, result);
        }

        public override void Interact()
        {
            if (!_saveStateSystem.GetState(SaveFirstDialogue))
            {
                SayVodyan("Здравствуй, Яговна! Видел, как ты летела — забыла, что у тебя для этого ступа есть?");
                SayAga("Признавайся, рыба дохлая, ты моей дочурке помог избушку заколдовать?");
                SayVodyan("Я, Яговна, я! Мне на этих болотах так скучно — хоть топись. Она как предложила твою избушку закрутить, я даже не раздумывал. Хоть на что-то новенькое погляжу.");
                SayAga("Вот нечисть пошла — ради хлеба и зрелищ готовы погромы учинять и имущество портить.");

                _saveStateSystem.SaveState(SaveFirstDialogue, true);
            }
            else
            {
                if (_inventorySystem.HasItem(_saucer))
                {
                    SayAga("Держи. Погляди на что-то, кроме своих болот — и избушку мою больше не тронь.");
                    SayVodyan("Спасибо, Яговна, удружила! Сниму я свои чары с избушки.");
                    SayVodyan("Вот, держи, налови себе рыбки.");
                    SayAga("И как же энтим пользоваться?");
                    SayVodyan("Ткни острым концом.");
                    
                    _inventorySystem.RemoveItem(_saucer);
                    _inventorySystem.TryAddItem(_harpun);
                }
                else
                {
                    SayVodyan("Поговорил бы кто со мной…");
                }
            }
        }
    }
}