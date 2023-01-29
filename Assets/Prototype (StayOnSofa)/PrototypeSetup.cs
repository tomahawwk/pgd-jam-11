using Prototype.Plugins.FadeOutSystem;
using UnityEngine;

namespace Prototype
{
    public class PrototypeSetup : MonoBehaviour
    {
        private FadeOutSystem _fadeOutSystem => FadeOutSystem.Instance;
        
        private void Start()
        {
            Screen.SetResolution(1280, 720, FullScreenMode.Windowed);
            _fadeOutSystem.OnlyOutFade(null);
        }
    }
}