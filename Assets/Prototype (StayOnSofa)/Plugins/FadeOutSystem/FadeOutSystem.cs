using System;
using System.Collections;
using Dialogue;
using UnityEngine;

namespace Prototype.Plugins.FadeOutSystem
{
    public class FadeOutSystem : MonoSingleton<FadeOutSystem>
    {
        private const string ResourcesPathToMenuPrefab = "Defaults/FadeOutMenu";

        [SerializeField] private float _fadeOutSpeed = 0.65f;
        [SerializeField] private FadeOutMenu _menuPrefab;
        private FadeOutMenu _menu;
        
        private bool _alreadyInUse;
        public bool IsOpen() => _alreadyInUse;
        
        private void Awake()
        {
            if (_menuPrefab == null)
                _menuPrefab = Resources.Load<FadeOutMenu>(ResourcesPathToMenuPrefab);
        }
        
        private void Init()
        {
            if (_menu == null)
                _menu = Instantiate(_menuPrefab);
        }
        
        public void FadeOut(Action action)
        {
            StartCoroutine(RoutineFadeOut(action));
        }

        public void OnlyOutFade(Action action)
        {
            StartCoroutine(RoutineOutFade(action));
        }

        private IEnumerator InFade()
        {
            _menu.SetAlpha(0);
            
            float alpha = 0;
            while (alpha < 1f)
            {
                alpha += Time.deltaTime * _fadeOutSpeed;
                _menu.SetAlpha(alpha);
                yield return null;
            }
            
            _menu.SetAlpha(1f);
        }

        private IEnumerator OutFade()
        {
            float alpha = 1f;
            _menu.SetAlpha(1f);
            
            while (alpha > 0f)
            {
                alpha -= Time.deltaTime * _fadeOutSpeed;
                _menu.SetAlpha(alpha);
                yield return null;
            }
            
            _menu.SetAlpha(0);
        }

        private IEnumerator RoutineOutFade(Action action)
        {
            _alreadyInUse = true;
            Init();
            _menu.SetLock(true);
            yield return OutFade();
            action?.Invoke();
            yield return null;
            _menu.SetLock(false);
            _alreadyInUse = false;
        }
        
        private IEnumerator RoutineFadeOut(Action action)
        {
            _alreadyInUse = true;
            Init();
            _menu.SetLock(true);
            yield return InFade();
            _alreadyInUse = false;
            action?.Invoke();
            yield return null;
            _menu.SetLock(false);
            yield return OutFade();
        }
    }
}