﻿using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Dialogue
{
    public enum DialogueType
    {
        Simple,
        Question,
        Avatar,
    }

    public class DialogueMenu : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _simpleDialogue;
        [SerializeField] private TextMeshProUGUI _avatarDialogue;
        [SerializeField] private TextMeshProUGUI _questionDialogue;
        
        [SerializeField] private GameObject _simpleDialogueLayer;
        [SerializeField] private GameObject _avatarDialogueLayer;
        [SerializeField] private GameObject _questionDialogueLayer;
        
        [SerializeField] private DialogueQuestion _dialogueQuestion1;
        [SerializeField] private DialogueQuestion _dialogueQuestion2;

        [SerializeField] private TextMeshProUGUI _titleAvatar;
        [SerializeField] private TextMeshProUGUI _titleQuestion;
        
        [SerializeField] private Image _avatar;
        [SerializeField] private GameObject _hover;

        private DialogueType _type = DialogueType.Simple;
        
        private TextMeshProUGUI _text;
        
        private void Awake() => DontDestroyOnLoad(gameObject);
        
        public void SetText(string value)
        {
            if (_text == null)
                _text = _simpleDialogue;

            _text.text = value;
        }

        public void SetHoverVisible(bool value) => _hover.SetActive(value);
        public void SetAvatar(Sprite sprite) => _avatar.sprite = sprite;

        public void SetAvatarTitle(string text) => _titleAvatar.text = text;

        public void SetQuestionTitle(string text) => _titleQuestion.text = text;
        
        public DialogueQuestion GetQuestion1() => _dialogueQuestion1;
        public DialogueQuestion GetQuestion2() => _dialogueQuestion2;

        public DialogueType GetMenuType() => _type;

        public void SetMenuType(DialogueType type)
        {
            SetText(string.Empty);
            
            _simpleDialogueLayer.SetActive(false);
            _avatarDialogueLayer.SetActive(false);
            _questionDialogueLayer.SetActive(false);

            _type = type;
            
            switch(type)
            {
                case DialogueType.Simple:
                {
                    _simpleDialogueLayer.SetActive(true);
                    _text = _simpleDialogue;
                    
                    break;
                }
                case DialogueType.Avatar:
                {
                    SetAvatarTitle(string.Empty);
                    
                    _avatarDialogueLayer.SetActive(true);
                    _text = _avatarDialogue;
                    
                    break;
                }
                case DialogueType.Question:
                {
                    SetQuestionTitle(string.Empty);
                    
                    _dialogueQuestion1.SetText(string.Empty);
                    _dialogueQuestion2.SetText(string.Empty);
                    
                    _questionDialogueLayer.SetActive(true);
                    _text = _questionDialogue;
                    
                    break;
                }
            }
        }
    }
}