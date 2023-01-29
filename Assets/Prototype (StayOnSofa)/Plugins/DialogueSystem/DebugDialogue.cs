using UnityEngine;

namespace Dialogue
{
    public class DebugDialogue : MonoBehaviour
    {
        [SerializeField] private Sprite _fistPersona;
        [SerializeField] private Sprite _secondPersona;
        
        private DialogueSystem _dialogue => DialogueSystem.Instance;
        
        private void Start()
        {
            
            _dialogue.Dialogue("Тьма просочилась как <color=red>вор</color> в ночи, украла свет и " +
                               "оставившая <color=red>землю</color> в состоянии постоянной мрака.");
            
            _dialogue.Dialogue("Небо было тяжелым от грозовых облаков, " +
                                   "окрашивающих ужасный <color=red>красный</color> свет на искаженных деревьях и скрипучих кустах.");
            
            _dialogue.Dialogue("Жители сжимались в своих домах, боясь выйти на неизвестные <color=#6a40a8>ужасы</color>, " +
                                   "которые прятались за краем их мигающего света свечи.");
            
            
            
            _dialogue.DialogueAvatar("Крикун", "АААААААААААААААААААААОАОАОАОАОАААААААА!!!!", _fistPersona);
            _dialogue.DialogueAvatar("Кот", "Ты че <color=red>ОРЕШЬ</color> то?", _secondPersona);
            
            
            _dialogue.DialogueDoubleQuestion("Бездна", "Назови любимое живонтное?",
                "Единорог", "Ящерица", result =>
                {
                    if (result)
                    {
                        _dialogue.DialogueDoubleQuestion("Бездна" ,"Ты точно в этом уверен?",
                            "ДА", "Нет все же ящерица", result =>
                            {
                                if (result)
                                    _dialogue.Dialogue("Пиздец");
                                else
                                    _dialogue.Dialogue("Хорошо!");
                            });
                    }
                    else
                    {
                        _dialogue.Dialogue("Хорошо!");
                    }
                }
            );
        }
        
    }
}