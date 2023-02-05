using System.Collections;
using Dialogue;
using Prototype;
using Prototype.Home.Details;
using Prototype.Logic.CameraUtils;
using Prototype.Plugins.FadeOutSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class InteractiveChangeScene : Interactive
{
    private static int _sceneNumber;

    [SerializeField] private AudioSource _audioCrows;
    private AudioSource _audio => GetComponent<AudioSource>();
    private DialogueSystem _dialogue => DialogueSystem.Instance;
    private FadeOutSystem _fadeOut => FadeOutSystem.Instance;

    [SerializeField] private LightsRotate _lights;
    [SerializeField] private ShakeCamera _shakeCamera;
        
    [SerializeField] private string _title;
    [SerializeField] [TextArea] private string _property;

    [SerializeField] private string _positive;
    [SerializeField] private string _negative;
    [SerializeField] private string _afterScene;

    [SerializeField] private AudioClip _turn;
    [SerializeField] private AudioClip _open;
    
    
    [SerializeField] private string[] _scenes;
    private string _sceneToApply;

    private bool _isUsed;
    
    public override void Interact()
    {
        if (_isUsed) return;
        
        _dialogue.DialogueDoubleQuestion(_title, _property, _positive, _negative, result =>
        {
            if (result)
            {
                _isUsed = true;
                
                var scene = _scenes[_sceneNumber];
                _sceneNumber += 1;

                if (_sceneNumber > _scenes.Length - 1)
                    _sceneNumber = 0;

                _sceneToApply = scene;

                StartCoroutine(RoutineChangeSceneLogic());
            }
        });
    }

    private IEnumerator RoutineChangeSceneLogic()
    {
        _audio.clip = _turn;
        _audio.Play();
        _audioCrows.Play();
        
        _dialogue.Dialogue(_afterScene);

        _shakeCamera.Play();
        yield return _lights.CoroutinePlay();
        
        while (_dialogue.IsOpen())
            yield return null;
        
        _audio.clip = _open;
        _audio.Play();
        
        _fadeOut.FadeOut(() =>
        {
            SceneManager.LoadScene(_sceneToApply); 
        });
    }
}