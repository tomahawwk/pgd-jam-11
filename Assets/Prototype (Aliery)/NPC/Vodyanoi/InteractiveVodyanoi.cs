using Dialogue;
using SaveState;
using Prototype.Plugins.FadeOutSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Prototype
{
    public class InteractiveVodyanoi : Interactive
    {
        private DialogueSystem _dialogue => DialogueSystem.Instance;
        private FadeOutSystem _fadeOut => FadeOutSystem.Instance;
        private SaveStateSystem _interactiveKey => SaveStateSystem.Instance;

        [SerializeField] private Sprite _vodyanoi;
        [SerializeField] private Sprite _babaYaga;

        private bool _firstDialogue = true;

        private void Vodyanoi(string text)
        {
            _dialogue.DialogueAvatar("Водяной", text, _vodyanoi);
        }

        private void BabaYaga(string text)
        {
            _dialogue.DialogueAvatar("Баба Яга", text, _babaYaga);
        }

        public override void Interact()
        {
            if (_firstDialogue)
            {
                _firstDialogue = false;


                Vodyanoi("Здравствуй, Яговна! Видел, как ты летела — забыла, что у тебя для этого ступа есть?");
                BabaYaga("Признавайся, рыба дохлая, ты моей дочурке помог избушку заколдовать?");
                Vodyanoi("Я, Яговна, я! Мне на этих болотах так скучно — хоть топись. Она как предложила твою избушку закрутить, я даже не раздумывал. Хоть на что-то новенькое погляжу.");
                BabaYaga("Вот нечисть пошла — ради хлеба и зрелищ готовы погромы учинять и имущество портить.");


            }
            else
            {
                Vodyanoi("Поговорил бы кто со мной…");
            }
            //    _dialogue.DialogueDoubleQuestion(_catByun, "Кот Баюн", "Чем помочь, хозяйка?",
            //    "Лестница", "Ничем, пока", result =>
            //    {
            //        if (result)
            //        {
            //            _dialogue.DialogueDoubleQuestion(_catByun, "Кот Баюн", "Вот и дожила ты, старая, до того, что сама по деревьям лазаешь…",
            //            "О, спасибо", "Мог бы и помочь!", result =>
            //            {
            //                if (result)
            //                    _dialogue.DialogueAvatar("Кот Баюн", "Незачто", _catByun);
            //                else
            //                    _dialogue.DialogueAvatar("Кот Баюн", "Сама справишься!", _catByun);
            //            });
            //        }
            //        else
            //        {
            //            _dialogue.DialogueAvatar("Кот Баюн", "Бывай!", _catByun);
            //        }
            //    }
            //);
            //}
        }
    }
}