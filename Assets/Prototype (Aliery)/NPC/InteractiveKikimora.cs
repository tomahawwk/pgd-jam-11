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
        private const string KikimoraTitle = "��������";
        private const string YagaTitle = "���";

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
                SayAga("�� ����������, ������ ������! �� ���� ���������, � ��� ���� ������� ��� ������!");
                SayKiki("����� �� �� ���, �������? ������-�� �� �� ��������� � ����������! � � ������ � ��� ��� ����� � ������ ��������.");
                SayAga("� � ����� ��� ��� �� �� ����� ��������� ������ ���� ������?");
                SayKiki("� � ������� �� ����� �����! � ��� � ��������, � ����������� ��������. ���� ����� �������� � ������� �� ��� ������ �������, ������� ���-������, �� �� ����� ���������.");
                SayAga("������ �������, � ����? ��� ��� �, �� ����� ���� ������ ������ �����?");
                SayKiki("���� ��, ������. � ������ � ������� ������� ����� ������� ����������� � ������� ����� ����� ����, � �� ��� �� ������ ������� �����, ���� �����!");

                _saveStateSystem.SaveState(SaveFirstDialogue, true);
            }
            else
            {
                if (_inventorySystem.HasItem((_flower)))
                {
                    SayAga("��� ����, ������ ������, �������� ��������. � �� ���� �� �����, �� ����� ����� � �� �� ��� ����� ������� � � ��� � ��� ��������, � � ���� ������� �� �� ����.");
                    SayKiki("��, ����� ����! �������, ���, �������. ��������� � ���� ���������, ��� � ����. ���, ������� ������� � ��� ��� �� ������ �� �����������.");
                    _inventorySystem.RemoveItem(_flower);
                    _inventorySystem.TryAddItem(_viteyka);
                }
                else
                {
                    SayKiki("�����, �� ������ ��� ������� � ����� ���� ��������, ���� �������� �����-������ �������");
                }
            }
        }
    }
}