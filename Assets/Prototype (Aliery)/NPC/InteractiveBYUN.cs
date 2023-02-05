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
                var item = GetItem();
                if (item != null)
                {
                    SayBun("��� ������, �������?",
                        item.Title, "�����, ����", result =>
                        {
                            if (result)
                            {
                                DialogeForItem(item);
                            }
                            else
                            {
                                SayBun("�����!");
                            }
                        });
                }
                else
                {
                    SayBun("�����!");
                }
            }
        }

        private void DialogeForItem(Item item)
        {
            if (item == _axe)
            {
                SayBun("����� � ������� �� ����-������ �������, ����� ���  ��������� ����� �����!");
            }
            else if (item == _apple)
            {
                SayBun("� ��� �� �� ����������, �������, ���-�� � �� �����, �� ����� �� ����. ����� �� ������� ����������� ������?");
            }
            else if (item == _saucer)
            {
                SayBun("� ���������� ������ �� ������� ����������...");
            }
            else if (item == _appleSilverPlate)
            {
                SayBun("���� ����� ����� ������ � ������� �� ��� ���-������, ����� ���� �� ������� �� ������� ���������. ��� �, �������, ��� ������� �������� �� ��������� ����������?");
            }
            else if (item == _stair)
            {
                SayBun("��� � ������ ��, ������, �� ����, ��� ���� �� �������� ��������");
            }
            else if (item == _harpun)
            {
                SayBun("� �� ������, �� ���� ������ �������� ����� ���� �������� ������� ����� � �����!");
            }
            else if (item == _goldfish)
            {
                SayBun("��������� ��, �����, ����� �� ����������. �� ������� �������, �� ������� ������, �� ������� �������� ���� �� ������ ����� ����������.");
                
                SayBun("������ �����?",
                    "�����", "�� �����", result =>
                    {
                        if (result)
                        {
                            _inventorySystem.RemoveItem(item);
                            SayBun("������� �����? ��, �������� ����������, �� � ����� ��� ������� �� ���� ������������. �����, � ������ ��� ����� ������� ���������?");
                            SayBun("�� �����-��, ����! ���� � � ����� ������� ����� ���. ��, �����, ���� � ������� �� �����, �� ���� ���� �������� ����������.");
                        }
                        else
                        {
                            SayBun("���� ��, �����, �������, �� �� ������ ���������.");
                        }
                    });

            }
            else if (item == _yarn)
            {
                SayBun("������� ����? ��������� � ����� �������� �������� �������, ���, ������ ���� ������ ��� �� ����, �� �, �������, �������.");
            }
            else if (item == _viteyka)
            {
                SayBun("��� ������� � �� ���� ���� �������, ������ ������ �� ����: ��� ��� �� ��� ���������� �� ������� ���������� ��������?");
            }
            else if (item == _magicSphere)
            {
                SayBun("��� ������� � �� ���� ���� �������, ������ ������ �� ����: ��� ��� �� ��� ���������� �� ������� ���������� ��������?");
            }
            else if (item == _leyka)
            {
                SayBun("�������, � ���-�� ��� ����� �������� ������. ���� ��� ������, �����, �������� ���-�� ������?");
            }
            else if (item == _flower)
            {
                SayBun("����� �� �� �������� �����? ������� ����� ����� �������, �� � �������.");
            }
        }
    }
}