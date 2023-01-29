using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dialogue
{
    public class DialogueSystem : MonoSingleton<DialogueSystem>
    {
        private const string ResourcesPathToMenuPrefab = "Defaults/DialoguePrefab";

        [SerializeField] private DialogueMenu _menuPrefab;
        private DialogueMenu _menu;

        [SerializeField] private float _timerPerLetter = 0.05f;
        [SerializeField] private float _fastTimerPerLetter = 0.01f;
        
        [SerializeField] private float _timerPerNextSlide = 1.5f;
        [SerializeField] private float _timeToAutoClose = 3f;

        
        private List<IEnumerator> _dialogueQueue;
        
        private WaitForSeconds _waitTillLetter;
        private WaitForSeconds _waitTillFastLetter;
        
        private WaitForSeconds _waitTillNextSlide;

        private bool _alreadyInUse;

        public bool IsOpen() => _alreadyInUse;

        private void Awake()
        {
            _dialogueQueue = new();
            
            _waitTillLetter = new WaitForSeconds(_timerPerLetter);
            _waitTillNextSlide = new WaitForSeconds(_timerPerNextSlide);

            _waitTillFastLetter = new WaitForSeconds(_fastTimerPerLetter);
        }

        private void Init()
        {
            if (_menuPrefab == null)
                _menuPrefab = Resources.Load<DialogueMenu>(ResourcesPathToMenuPrefab);

            if (_menu == null)
                _menu = Instantiate(_menuPrefab);
        }

        private void Close()
        {
            if (_menu != null)
                Destroy(_menu.gameObject);
        }

        private IEnumerator WaitAnyKey(bool useBreakTimer = true)
        {
            float timer = 0;
            
            while (true)
            {
                timer += Time.deltaTime;
                if (timer > _timeToAutoClose && useBreakTimer)
                    break;

                if (Input.anyKeyDown)
                    break;

                yield return null;
            }
        }

        private IEnumerator FillText(string text)
        {
            var waitMethod = _waitTillLetter;
            
            string textBuffer = "";

            var words = text.Split(' ');

            for (int i = 0; i < words.Length; i++)
            {
                var word = words[i];
                
                if (word.StartsWith("<color="))
                {
                    int close = word.IndexOf(">", StringComparison.Ordinal) + 1;
                    
                    string colorHeader = word.Substring(0, close);
                    string colorEnder = "</color>";
                    
                    int startWord = word.IndexOf(">", StringComparison.Ordinal) + 1;
                    int endWord = word.LastIndexOf("<", StringComparison.Ordinal);
                    string parseWord = word.Substring(startWord, endWord - startWord);

                    for (int j = 0; j < parseWord.Length; j++)
                    {
                        char letter = parseWord[j];
                        textBuffer += $"{colorHeader}{letter}{colorEnder}";
                        
                        _menu.SetText(textBuffer);
                        yield return waitMethod;
                    }
                }
                else
                {
                    for (int j = 0; j < word.Length; j++)
                    {
                        if (Input.anyKey && textBuffer.Length > 10)
                            waitMethod = _waitTillFastLetter;
                        
                        var letter = word[j];
                        textBuffer += letter;
                        _menu.SetText(textBuffer);
                        
                        yield return waitMethod;
                    }
                }

                if (i != words.Length - 1)
                    textBuffer += " ";
            }
        }

        private IEnumerator SimpleDialogue(string text)
        {
            _menu.SetMenuType(DialogueType.Simple);
            yield return FillText(text);
            yield return _waitTillNextSlide;
        }

        private IEnumerator SimpleDialogueAvatar(string text, string name, Sprite avatar)
        {
            _menu.SetMenuType(DialogueType.Avatar);
            _menu.SetAvatarTitle(name);
            
            _menu.SetHoverVisible(true);
            _menu.SetAvatar(avatar);
            yield return FillText(text);
            yield return null;

            yield return WaitAnyKey(false);
            
            _menu.SetText(String.Empty);
            _menu.SetHoverVisible(false);
        }
        

        //Bug: need check new one for bugs
        private IEnumerator OldSimpleDialogue(string text)
        {
            var waitMethod = _waitTillLetter;
            
            string textBuffer = "*";

            for (int i = 0; i < text.Length; i++)
            {
                if (Input.anyKey && textBuffer.Length > 5)
                    waitMethod = _waitTillFastLetter;
                
                char letter = text[i];
                textBuffer += letter;

                _menu.SetText(textBuffer);
                yield return waitMethod;
            }
        }

        private IEnumerator SimpleDialogueDoubleQuestion(string text, string name, string question1, string question2, Action<bool> result)
        {
            _menu.SetMenuType(DialogueType.Question);
            _menu.SetQuestionTitle(name);
            
            yield return FillText(text);
            bool answerIsDone = false;
            
            _menu.SetHoverVisible(true);
            
            _menu.GetQuestion1().Create(question1, () =>
            {
                result?.Invoke(true);
                answerIsDone = true;
            });
            
            _menu.GetQuestion2().Create(question2, () =>
            {
                result?.Invoke(false);
                answerIsDone = true;
            });

            while (!answerIsDone)
                yield return null;

            yield return null;
            
            _menu.SetHoverVisible(false);
        }

        public void Dialogue(string text)
        {
            _dialogueQueue.Add(SimpleDialogue(text));
        }

        public void DialogueAvatar(string name, string text, Sprite sprite)
        {
            _dialogueQueue.Add(SimpleDialogueAvatar(text, name, sprite));
        }
        
        public void DialogueDoubleQuestion(string name, string text, string question1, string question2, Action<bool> result)
        {
            _dialogueQueue.Add(SimpleDialogueDoubleQuestion(text, name, question1, question2, result));
        }

        private IEnumerator AllDialogueCoroutine(IEnumerable<IEnumerator> dialogues)
        {
            _alreadyInUse = true;
            Init();

            foreach (var dialogue in dialogues)
            {
                yield return dialogue;
            }

            _menu.SetHoverVisible(true);
            
            if (_menu.GetMenuType() != DialogueType.Question)
                yield return WaitAnyKey();
            
            _alreadyInUse = false;
        }

        private void PrepareDialogueQueue()
        {
            if (_alreadyInUse) return;
            
            bool queueEmpty = !(_dialogueQueue.Count > 0);

            if (!queueEmpty)
            {
                StartCoroutine(AllDialogueCoroutine(_dialogueQueue.ToArray()));
                _dialogueQueue.Clear();
            }
            
            if (queueEmpty)
                Close();
        }
        
        private void Update()
        {
            PrepareDialogueQueue();
        }
    }
}