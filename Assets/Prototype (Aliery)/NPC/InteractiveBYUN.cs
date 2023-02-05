using Dialogue;
using Prototype.Plugins.FadeOutSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Prototype
{
    public class InteractiveBYUN : Interactive
    {
        private DialogueSystem _dialogue => DialogueSystem.Instance;
        private FadeOutSystem _fadeOut => FadeOutSystem.Instance;

        [SerializeField] private Sprite _catByun;
        [SerializeField] private Sprite _babaYaga;

        private bool _firstDialogue=true;

        public override void Interact()
        {
            if (_firstDialogue)
            {
                _firstDialogue = false;
                _dialogue.DialogueAvatar("Кот Баюн", "С добрым утречком, хозяйка зверей, покровительница людей! Как спалось? Кости на погоду не ломило?", _catByun);
                _dialogue.DialogueAvatar("Баба Яга", "Уж не ты ли, пройдоха, свою лапу тут приложил, да избушку околдовал?", _babaYaga);
                _dialogue.DialogueAvatar("Кот Баюн", "Не я, хозяйка, не я. Но может и следовало — давно ты меня <color=yellow>рыбкой</color> не потчевала, на печи давно полежать не пускала…", _catByun);
                _dialogue.DialogueAvatar("Баба Яга", "А чьих это рук дело, стало быть?", _babaYaga);
                _dialogue.DialogueAvatar("Кот Баюн", "А они все рядом тут — ты оглядись по всем четырем сторонам, да под ноги поглядывай. Думается мне, из избушки не только ты выпала, но и всяческое барахло.", _catByun);
                _dialogue.DialogueAvatar("Кот Баюн", "Авось, пригодится чего-нибудь и соседям твоим, тогда они сведут свое колдовство.", _catByun);
            }
            else
            {
                _dialogue.DialogueDoubleQuestion(_catByun, "Кот Баюн", "Чем помочь, хозяйка?",
                "Лестница", "Ничем, пока", result =>
                {
                    if (result)
                    {
                        _dialogue.DialogueDoubleQuestion(_catByun, "Кот Баюн", "Вот и дожила ты, старая, до того, что сама по деревьям лазаешь…",
                        "О, спасибо", "Мог бы и помочь!", result =>
                        {
                        if (result)
                            _dialogue.DialogueAvatar("Кот Баюн", "Незачто", _catByun);
                        else
                            _dialogue.DialogueAvatar("Кот Баюн", "Сама справишься!", _catByun);
                        });
                    }
                    else
                    {
                        _dialogue.DialogueAvatar("Кот Баюн", "Бывай!", _catByun);
                    }
                }
            );
            }
        }
    }
}