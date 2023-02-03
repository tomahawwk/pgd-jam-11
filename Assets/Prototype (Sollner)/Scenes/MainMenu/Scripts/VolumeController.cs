using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    [SerializeField] private AudioMixer _mixer;
    [SerializeField] private Slider _masterVolume;
    [SerializeField] private Slider _musicVolume;
    [SerializeField] private Slider _effectsVolume;

    public const string MASTER_VOLUME = "Master_Volume";
    public const string MUSIC_VOLUME = "Music_Volume";
    public const string EFFECTS_VOLUME = "Effects_Volume";

    private void Awake()
    {
        _masterVolume.onValueChanged.AddListener(SetMasterVolume);
        _musicVolume.onValueChanged.AddListener(SetMusicVolume);
        _effectsVolume.onValueChanged.AddListener(SetEffectsVolume);
    }

    private void Start()
    {
        _masterVolume.value = PlayerPrefs.GetFloat(AudioManager.MASTER_KEY, 1f);
        _musicVolume.value = PlayerPrefs.GetFloat(AudioManager.MUSIC_KEY, 1f);
        _effectsVolume.value = PlayerPrefs.GetFloat(AudioManager.EFFECTS_KEY, 1f);
    }

    private void OnDisable()
    {
        PlayerPrefs.SetFloat(AudioManager.MASTER_KEY, _masterVolume.value);
        PlayerPrefs.SetFloat(AudioManager.MUSIC_KEY, _musicVolume.value);
        PlayerPrefs.SetFloat(AudioManager.EFFECTS_KEY, _effectsVolume.value);
    }
    private void SetMasterVolume(float value)
    {
        _mixer.SetFloat(MASTER_VOLUME, MathF.Log10(value) * 20);
    }
    
    private void SetMusicVolume(float value)
    {
        _mixer.SetFloat(MUSIC_VOLUME, MathF.Log10(value) * 20);
    }
    
    private void SetEffectsVolume(float value)
    {
        _mixer.SetFloat(EFFECTS_VOLUME, MathF.Log10(value) * 20);
    }
    
}
