using UnityEngine;

namespace Dialogue
{
    public static class SingletonStats
    {
        public static bool IsQuitting;
    }

    public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;
        public static T Instance => GetInstance();

        public bool IsQuitting => SingletonStats.IsQuitting;

        public static void Create()
        {
            GetInstance();
        }

        private static bool _isQuitInit;

        private static T GetInstance()
        {
            if (!_isQuitInit)
            {
                Application.quitting += Quit;
                _isQuitInit = true;
            }

            if (!SingletonStats.IsQuitting)
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<T>();

                    if (_instance == null)
                    {
                        GameObject obj = new GameObject($"[{typeof(T).Name}]");
                        DontDestroyOnLoad(obj);
                        
                        _instance = obj.AddComponent<T>();
                    }
                }

                return _instance;
            }

            return null;
        }

        private static void Quit()
        {
            SingletonStats.IsQuitting = true;
        }

        private void OnApplicationQuit()
        {
            SingletonStats.IsQuitting = true;
        }

        private void OnDestroy()
        {
            SingletonStats.IsQuitting = true;
        }
    }
}