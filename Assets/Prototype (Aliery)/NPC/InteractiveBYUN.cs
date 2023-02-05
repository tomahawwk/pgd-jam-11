using Dialogue;
using Prototype.Logic;
using Prototype.Plugins.FadeOutSystem;
using SaveState;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Prototype
{
    [RequireComponent(typeof(AudioSource))]
    public class InteractiveBYUN : Interactive
    {
        private const string BunTitle = "Баюн";
        private const string YagaTitle = "Яга";

        private string SaveFirstDialogue = nameof(InteractiveBYUN) + "Quest";
        private string SaveHasItem = nameof(InteractiveBYUN) + "Has Quest Item";


        private AudioSource _audio => GetComponent<AudioSource>();
        private DialogueSystem _dialogueSystem => DialogueSystem.Instance;
        private SaveStateSystem _saveStateSystem => SaveStateSystem.Instance;
        private InventorySystem _inventorySystem => InventorySystem.Instance;
        private FadeOutSystem _fade => FadeOutSystem.Instance;

        [SerializeField] private Sprite _bunAvatar;
        [SerializeField] private Sprite _agaAvatar;

        [SerializeField] private List<Item> _items;

        [SerializeField] private Item _axe;
        [SerializeField] private Item _apple;
        [SerializeField] private Item _saucer;
        [SerializeField] private Item _appleSilverPlate;
        [SerializeField] private Item _stair;
        [SerializeField] private Item _harpun;
        [SerializeField] private Item _goldfish;
        [SerializeField] private Item _yarn;
        [SerializeField] private Item _viteyka;
        [SerializeField] private Item _magicSphere;
        [SerializeField] private Item _leyka;
        [SerializeField] private Item _flower;

        public Item GetItem()
        {
            foreach (var item in _items)
            {
                if (InventorySystem.Instance.HasItem(item) && !_saveStateSystem.GetState("HasItem: " + item.Title))
                {
                    _saveStateSystem.SaveState("HasItem: " + item.Title, true);
                    return item;
                }
            }

            return null;
        }

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


        public override void Interact()
        {
            if (!_saveStateSystem.GetState(SaveFirstDialogue))
            {
                SayBun("С добрым утречком, хозяйка зверей, покровительница людей! Как спалось? Кости на погоду не ломило?");
                AgaSay("Уж не ты ли, пройдоха, свою лапу тут приложил, да избушку околдовал?");
                SayBun("Не я, хозяйка, не я. Но может и следовало — давно ты меня <color=yellow>рыбкой</color> не потчевала, на печи давно полежать не пускала…");
                AgaSay("А чьих это рук дело, стало быть?");
                SayBun("А они все рядом тут — ты оглядись по всем четырем сторонам, да под ноги поглядывай. Думается мне, из избушки не только ты выпала, но и всяческое барахло.");
                SayBun("Авось, пригодится чего-нибудь и соседям твоим, тогда они сведут свое колдовство.");
                _saveStateSystem.SaveState(SaveFirstDialogue, true);
            }
            else
            {
                var item = GetItem();
                if (item != null)
                {
                    SayBun("Чем помочь, хозяйка?",
                        item.Title, "Ничем, пока", result =>
                        {
                            if (result)
                            {
                                DialogeForItem(item);
                            }
                            else
                            {
                                SayBun("Удачи!");
                            }
                        });
                }
                else
                {
                    SayBun("Удачи!");
                }
            }
        }

        private void DialogeForItem(Item item)
        {
            if (item == _axe)
            {
                SayBun("Давай в избушке по чему-нибудь долбанём, авось она  слушаться снова начнёт!");
            }
            else if (item == _apple)
            {
                SayBun("А как же ты дотянешься, хозяйка, что-то я ни ступы, ни метлы не вижу. Ужель по лесенке карабкаться будешь?");
            }
            else if (item == _saucer)
            {
                SayBun("В серебряном блюдце не хватает волшебства...");
            }
            else if (item == _appleSilverPlate)
            {
                SayBun("Этот водун такой унылый — всучить бы ему что-нибудь, чтобы хоть со стороны на веселье посмотрел. Где ж, хозяйка, твоё яблочко наливное на блюдечеке серебряном?");
            }
            else if (item == _stair)
            {
                SayBun("Вот и дожила ты, старая, до того, что сама по деревьям лазаешь…");
            }
            else if (item == _harpun)
            {
                SayBun("Я бы словил, да лапы марать неохота… Давай этим гарпуном наловим рыбки к обеду!");
            }
            else if (item == _goldfish)
            {
                SayBun("Давненько мы, бабка, ничем не лакомились. Ни молодца доброго, ни красной девицы, ни детишек румяных… Хоть бы рыбкой какой поживиться.");
                
                SayBun("Отдашь рыбку?",
                    "Держи", "Не отдам", result =>
                    {
                        if (result)
                        {
                            _inventorySystem.RemoveItem(item);
                            SayBun("Золотая рыбка? Ах, невелико угощеньице, но я готов его принять со всем великодушием. Может, я теперь сам смогу желания исполнять?");
                            SayBun("Ты глянь-ка, могу! Снял я с твоей каморки часть чар. Эх, бабка, хила и немощна ты стала, со всем тебе помогать приходится.");
                        }
                        else
                        {
                            SayBun("Тебя бы, бабка, слопать, да уж больно костлявая.");
                        }
                    });

            }
            else if (item == _yarn)
            {
                SayBun("Смотать нити? Наверняка у нашей прядуньи Кикиморы найдётся, чем, только тебе просто так не даст, ты её, хозяйка, обидела.");
            }
            else if (item == _viteyka)
            {
                SayBun("Вот сколько я на своём веку повидал, одного понять не могу: это как же при сматывании на витейку получается клубочек?");
            }
            else if (item == _magicSphere)
            {
                SayBun("Вот сколько я на своём веку повидал, одного понять не могу: это как же при сматывании на витейку получается клубочек?");
            }
            else if (item == _leyka)
            {
                SayBun("Хозяйка, я где-то тут видел засохший росток. Если его полить, может, вырастет что-то путное?");
            }
            else if (item == _flower)
            {
                SayBun("Давно ли ты поливала цветы? Глядишь какой сухой найдешь, да и польёшь.");
            }
        }
    }
}