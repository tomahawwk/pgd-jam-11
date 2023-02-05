using System;
using Dialogue;
using Prototype.Plugins.FadeOutSystem;
using SaveState;
using UnityEngine;

namespace Prototype.Logic
{
    [RequireComponent(typeof(AudioSource))]
    public class InteractiveFlower : Interactive
    {
        private const string BunTitle = "����";
        private const string YagaTitle = "���";

        private AudioSource _audio => GetComponent<AudioSource>();
        private DialogueSystem _dialogueSystem => DialogueSystem.Instance;
        private SaveStateSystem _saveStateSystem => SaveStateSystem.Instance;
        private InventorySystem _inventorySystem => InventorySystem.Instance;
        private FadeOutSystem _fade => FadeOutSystem.Instance;

        [SerializeField] private Sprite _bunAvatar;
        [SerializeField] private Sprite _agaAvatar;

        [SerializeField] private Item _leyka;
        [SerializeField] private Item _flower;

        private void SayBun(string text) => _dialogueSystem.DialogueAvatar(BunTitle, text, _bunAvatar);
        private void AgaSay(string text) => _dialogueSystem.DialogueAvatar(YagaTitle, text, _agaAvatar);

        private void SayBun(string text, string question1, string question2, Action<bool> result)
        {
            _dialogueSystem.DialogueDoubleQuestion(_bunAvatar, BunTitle, text, question1, question2, result);
        }

        private void AgaSay(string text, string question1, string question2, Action<bool> result)
        {
            _dialogueSystem.DialogueDoubleQuestion(_agaAvatar, YagaTitle, text, question1, question2, result);
        }

        public override void Interact()
        {


            if (!_inventorySystem.HasItem(_leyka))
            {
                AgaSay("��� ��� �� ���? ��� ���������� ������� ��� � �������� �����?");
            }
            else
            {
                AgaSay("��, ��� ��� �� �������? ��� � ���� ������ �� ����. ���� ����� ����-�� ������� � ���� ������, ��� � ���� � ������������ ��������� �����!", "������!", "����", result =>
                {
                    if (result)
                    {

                        _fade.FadeOut(() =>
                        {
                            _inventorySystem.RemoveItem(_leyka);
                            _inventorySystem.TryAddItem(_flower);
                            _audio.Play();
                        });
                    }
                });

            }

        }

    }
}
