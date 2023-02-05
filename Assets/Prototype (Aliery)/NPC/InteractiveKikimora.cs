using Dialogue;
using SaveState;
using Prototype.Plugins.FadeOutSystem;
using UnityEngine;
using System;

namespace Prototype
{
    [RequireComponent(typeof(AudioSource))]
    public class InteractiveKikimora : Interactive
    {
        private const string KikimoraTitle = "Кикимора";
        private const string YagaTitle = "Яга";

        private string SaveFirstDialogue = nameof(InteractiveKikimora) + "Quest";
        private string SaveHasItem = nameof(InteractiveKikimora) + "Has Quest Item";


        private AudioSource _audio => GetComponent<AudioSource>();
        private DialogueSystem _dialogueSystem => DialogueSystem.Instance;
        private SaveStateSystem _saveStateSystem => SaveStateSystem.Instance;
        private InventorySystem _inventorySystem => InventorySystem.Instance;
        private FadeOutSystem _fade => FadeOutSystem.Instance;

        [SerializeField] private Sprite _kikimoraAvatar;
        [SerializeField] private Sprite _agaAvatar;

        [SerializeField] private Item _flower;
        [SerializeField] private Item _viteyka;

        private void SayKiki(string text) => _dialogueSystem.DialogueAvatar(KikimoraTitle, text, _kikimoraAvatar);
        private void SayAga(string text) => _dialogueSystem.DialogueAvatar(YagaTitle, text, _agaAvatar);

        private void SayKiki(string text, string question1, string question2, Action<bool> result)
        {
            _dialogueSystem.DialogueDoubleQuestion(_kikimoraAvatar, KikimoraTitle, text, question1, question2, result);
        }

        private void SayAga(string text, string question1, string question2, Action<bool> result)
        {
            _dialogueSystem.DialogueDoubleQuestion(_agaAvatar, YagaTitle, text, question1, question2, result);
        }

        public override void Interact()
        {
            if (!_saveStateSystem.GetState(SaveFirstDialogue))
            {
                SayAga("Ну здравствуй, чудище лесное! Ты чего малюешься, и так ведь страшна как смерть!");
                SayKiki("Зачем же вы так, бабушка? Ничего-то вы не понимаете в прекрасном! А я видела — так все девки в городе красятся.");
                SayAga("И с каких это пор ты на девок городских хочешь быть похожа?");
                SayKiki("А я вечером на танцы пойду! Я уже и верёвочку, и ковырялочку разучила. Буду самой красивой — подойдёт ко мне добрый молодец, подарит что-нибудь, да на танец пригласит.");
                SayAga("Добрый молодец, к тебе? Это что ж, на танцы даже совсем слепые ходят?");
                SayKiki("Злая вы, бабуль. Я потому и помогла дочурке вашей избушку заколдовать — сколько веков рядом живём, а от вас ни одного доброго слова, одна брань!");

                _saveStateSystem.SaveState(SaveFirstDialogue, true);
            }
            else
            {
                if (_inventorySystem.HasItem((_flower)))
                {
                    SayAga("Вот тебе, чудище лесное, цветочек аленький. И не ходи на танцы, не пугай людей — не то ещё охоту объявят и к нам в лес заявятся, а у меня избушка не на ходу.");
                    SayKiki("Ах, какое диво! Спасибо, вам, бабушка. Расколдую я вашу избушечку, так и быть. Вот, держите витейку — мне она на танцах не понадобится.");
                    _inventorySystem.RemoveItem(_flower);
                    _inventorySystem.TryAddItem(_viteyka);
                }
                else
                {
                    SayKiki("Может, на танцах мне впервые в жизни хоть коврижку, хоть цветочек какой-нибудь подарят…");
                }
            }
        }
    }
}