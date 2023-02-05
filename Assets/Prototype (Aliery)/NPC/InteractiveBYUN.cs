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
                _dialogue.DialogueAvatar("��� ����", "� ������ ��������, ������� ������, ��������������� �����! ��� �������? ����� �� ������ �� ������?", _catByun);
                _dialogue.DialogueAvatar("���� ���", "�� �� �� ��, ��������, ���� ���� ��� ��������, �� ������� ���������?", _babaYaga);
                _dialogue.DialogueAvatar("��� ����", "�� �, �������, �� �. �� ����� � ��������� � ����� �� ���� <color=yellow>������</color> �� ���������, �� ���� ����� �������� �� ��������", _catByun);
                _dialogue.DialogueAvatar("���� ���", "� ���� ��� ��� ����, ����� ����?", _babaYaga);
                _dialogue.DialogueAvatar("��� ����", "� ��� ��� ����� ��� � �� �������� �� ���� ������� ��������, �� ��� ���� ����������. �������� ���, �� ������� �� ������ �� ������, �� � ��������� �������.", _catByun);
                _dialogue.DialogueAvatar("��� ����", "�����, ���������� ����-������ � ������� �����, ����� ��� ������ ���� ����������.", _catByun);
            }
            else
            {
                _dialogue.DialogueDoubleQuestion(_catByun, "��� ����", "��� ������, �������?",
                "��������", "�����, ����", result =>
                {
                    if (result)
                    {
                        _dialogue.DialogueDoubleQuestion(_catByun, "��� ����", "��� � ������ ��, ������, �� ����, ��� ���� �� �������� ��������",
                        "�, �������", "��� �� � ������!", result =>
                        {
                        if (result)
                            _dialogue.DialogueAvatar("��� ����", "�������", _catByun);
                        else
                            _dialogue.DialogueAvatar("��� ����", "���� ����������!", _catByun);
                        });
                    }
                    else
                    {
                        _dialogue.DialogueAvatar("��� ����", "�����!", _catByun);
                    }
                }
            );
            }
        }
    }
}