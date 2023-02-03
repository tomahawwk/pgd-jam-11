using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] private AudioMixer _mixer;
    
    public const string MASTER_KEY = "masterVolume";
    public const string MUSIC_KEY = "musicVolume";
    public const string EFFECTS_KEY = "effectsVolume";

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        LoadVolume();
    }

    private void LoadVolume()
    {
        float masterVolume = PlayerPrefs.GetFloat(MASTER_KEY, 1f);
        float musicVolume = PlayerPrefs.GetFloat(MUSIC_KEY, 1f);
        float effectsVolume = PlayerPrefs.GetFloat(EFFECTS_KEY, 1f);
        _mixer.SetFloat(VolumeController.MASTER_VOLUME, MathF.Log10(masterVolume) * 20);
        _mixer.SetFloat(VolumeController.MUSIC_VOLUME, MathF.Log10(musicVolume) * 20);
        _mixer.SetFloat(VolumeController.EFFECTS_VOLUME, MathF.Log10(effectsVolume) * 20);
    }
}
