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
        private const string BunTitle = "����";
        private const string YagaTitle = "���";

        private string SaveFirstDialogue = nameof(InteractiveBYUN) + "Quest";
        private string SaveHasItem = nameof(InteractiveBYUN) + "Has Quest Item";

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

        [SerializeField] private List<Item> _items;

        public Item GetItem()
        {
            foreach (var item in _items)
            {
                if (InventorySystem.Instance.HasItem(item))
                {
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

       

        private void Start()
        {
            CheckStateBun();
        }
        private void CheckStateBun()
        {
            if (_saveStateSystem.GetState(SaveFirstDialogue))
            {
                _openBake.SetActive(true);
                _closeBake.SetActive(false);
            }

            //if (_saveStateSystem.GetState(SaveHasItem))
            //{
            //    _bakeItem.SetActive(false);
            //}
        }

        public override void Interact()
        {
            if (!_saveStateSystem.GetState(SaveFirstDialogue))
            {
                SayBun("� ������ ��������, ������� ������, ��������������� �����! ��� �������? ����� �� ������ �� ������?");
                AgaSay("�� �� �� ��, ��������, ���� ���� ��� ��������, �� ������� ���������?");
                SayBun("�� �, �������, �� �. �� ����� � ��������� � ����� �� ���� <color=yellow>������</color> �� ���������, �� ���� ����� �������� �� ��������");
                AgaSay("� ���� ��� ��� ����, ����� ����?");
                SayBun("� ��� ��� ����� ��� � �� �������� �� ���� ������� ��������, �� ��� ���� ����������. �������� ���, �� ������� �� ������ �� ������, �� � ��������� �������.");
                SayBun("�����, ���������� ����-������ � ������� �����, ����� ��� ������ ���� ����������.");
                _saveStateSystem.SaveState(SaveFirstDialogue, true);
            }
            else
            {

                SayBun("��� ������, �������?",
                    "��������", "�����, ����", result =>
                    {
                        if (result)
                        {
                            SayBun("���� �����");
                        }
                        else
                        {
                            SayBun("�����!");
                        }
                    });
            }
        }
    }
}