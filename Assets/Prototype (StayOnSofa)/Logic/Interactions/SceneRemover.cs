using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Prototype.Logic.Interactions
{
    public class SceneRemover : MonoBehaviour
    {
        [SerializeField] private string[] _scenes;
        public static List<string> _activeScenes = new ();

        private void Start()
        {
            if (_activeScenes.Count == 0)
            {
                foreach (var scene in _scenes)
                    _activeScenes.Add(scene);
            }
        }

        public static string GetRandomScene()
        {
            if (_activeScenes.Count > 0)
            {
                return _activeScenes[Random.Range(0, _activeScenes.Count)];
            }

            return string.Empty;
        }

        public static void RemoveCurrentScene()
        {
            var sceneName = SceneManager.GetActiveScene().name;
            
            if (_activeScenes.Contains(sceneName))
                _activeScenes.Remove(sceneName);
        }
    }
}