using Dialogue;
using UnityEngine;

namespace Prototype
{
    public class PrototypeSetup : MonoBehaviour
    {
        private DialogueSystem _dialogue => DialogueSystem.Instance;
        
        private void Start()
        {
            Screen.SetResolution(1280, 720, FullScreenMode.Windowed);
            _dialogue.Dialogue("Тестовая сборка: 1");
        }
    }
}