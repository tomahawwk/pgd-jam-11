using UnityEngine;
using Prototype.Plugins.FadeOutSystem;
using UnityEngine.SceneManagement;

namespace Prototype
{
    public class MainMenu : MonoBehaviour
    {
        private FadeOutSystem _fadeOut => FadeOutSystem.Instance;

        [SerializeField] private string _sceneToLoad;

        public void LoadGameScene()
        {
            _fadeOut.FadeOut(() => SceneManager.LoadScene(_sceneToLoad));
        }

        public void ExitGame()
        {
            Application.Quit();
            UnityEditor.EditorApplication.isPlaying = false;
        }
    }
}