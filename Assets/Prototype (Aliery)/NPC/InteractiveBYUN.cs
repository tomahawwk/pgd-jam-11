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

        [SerializeField] private Sprite _fistPersona;
        [SerializeField] private Sprite _secondPersona;

        private bool _firstDialogue=true;

        public override void Interact()
        {
            if (_firstDialogue)
            {
                _firstDialogue = false;
                _dialogue.DialogueAvatar("��� ����", "� ������ ��������, ������� ������, ��������������� �����! ��� �������? ����� �� ������ �� ������?", _fistPersona);
                _dialogue.DialogueAvatar("���� ���", "�� �� �� ��, ��������, ���� ���� ��� ��������, �� ������� ���������?", _secondPersona);
                _dialogue.DialogueAvatar("��� ����", "�� �, �������, �� �. �� ����� � ��������� � ����� �� ���� <color=yellow>������</color> �� ���������, �� ���� ����� �������� �� ��������", _fistPersona);
                _dialogue.DialogueAvatar("���� ���", "� ���� ��� ��� ����, ����� ����?", _secondPersona);
                _dialogue.DialogueAvatar("��� ����", "� ��� ��� ����� ��� � �� �������� �� ���� ������� ��������, �� ��� ���� ����������. �������� ���, �� ������� �� ������ �� ������, �� � ��������� �������.", _fistPersona);
                _dialogue.DialogueAvatar("��� ����", "�����, ���������� ����-������ � ������� �����, ����� ��� ������ ���� ����������.", _fistPersona);
            }
            else
            {
                _dialogue.DialogueDoubleQuestion(_fistPersona, "��� ����", "��� ������, �������?",
                "��������", "�����, ����", result =>
                {
                    if (result)
                    {
                        _dialogue.DialogueDoubleQuestion(_fistPersona, "��� ����", "��� � ������ ��, ������, �� ����, ��� ���� �� �������� ��������",
                        "�, �������", "��� �� � ������!", result =>
                        {
                        if (result)
                            _dialogue.DialogueAvatar("��� ����", "�������", _fistPersona);
                        else
                            _dialogue.DialogueAvatar("��� ����", "���� ����������!", _fistPersona);
                        });
                    }
                    else
                    {
                        _dialogue.DialogueAvatar("��� ����", "�����!", _fistPersona);
                    }
                }
            );
            }
        }
    }
}