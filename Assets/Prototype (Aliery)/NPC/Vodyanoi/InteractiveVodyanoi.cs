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
            _dialogue.DialogueAvatar("�������", text, _vodyanoi);
        }

        private void BabaYaga(string text)
        {
            _dialogue.DialogueAvatar("���� ���", text, _babaYaga);
        }

        public override void Interact()
        {
            if (_firstDialogue)
            {
                _firstDialogue = false;


                Vodyanoi("����������, ������! �����, ��� �� ������ � ������, ��� � ���� ��� ����� ����� ����?");
                BabaYaga("�����������, ���� ������, �� ���� ������� ����� ������� �����������?");
                Vodyanoi("�, ������, �! ��� �� ���� ������� ��� ������ � ���� ������. ��� ��� ���������� ���� ������� ���������, � ���� �� ����������. ���� �� ���-�� ��������� �������.");
                BabaYaga("��� ������� ����� � ���� ����� � ������ ������ ������� ������� � ��������� �������.");


            }
            else
            {
                Vodyanoi("��������� �� ��� �� ����");
            }
            //    _dialogue.DialogueDoubleQuestion(_catByun, "��� ����", "��� ������, �������?",
            //    "��������", "�����, ����", result =>
            //    {
            //        if (result)
            //        {
            //            _dialogue.DialogueDoubleQuestion(_catByun, "��� ����", "��� � ������ ��, ������, �� ����, ��� ���� �� �������� ��������",
            //            "�, �������", "��� �� � ������!", result =>
            //            {
            //                if (result)
            //                    _dialogue.DialogueAvatar("��� ����", "�������", _catByun);
            //                else
            //                    _dialogue.DialogueAvatar("��� ����", "���� ����������!", _catByun);
            //            });
            //        }
            //        else
            //        {
            //            _dialogue.DialogueAvatar("��� ����", "�����!", _catByun);
            //        }
            //    }
            //);
            //}
        }
    }
}