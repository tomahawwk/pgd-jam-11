using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown _resolutionDropdown;

    private Resolution[] _resolutions;
    private const string RESOLUTION = "ResolutionPreference";
    private const string FULLSCREEN = "FullscreenPreference";

    private void Awake()
    {
        _resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        _resolutions = Screen.resolutions;
        int currentResolutionIndex = 0;
        for (int i = 0; i < _resolutions.Length; i++)
        {
            string option = _resolutions[i].width + "x" + _resolutions[i].height + " " + _resolutions[i].refreshRate +
                            "Hz";
            options.Add(option);
            if (_resolutions[i].width == Screen.currentResolution.width &&
                _resolutions[i].height == Screen.currentResolution.height)
                currentResolutionIndex = i;
        }
        _resolutionDropdown.AddOptions(options);
        _resolutionDropdown.RefreshShownValue();
        LoadSettings(currentResolutionIndex);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = _resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SaveSettings()
    {
        PlayerPrefs.SetInt(RESOLUTION, _resolutionDropdown.value);
        PlayerPrefs.SetInt(FULLSCREEN, System.Convert.ToInt32(Screen.fullScreen));
    }

    public void LoadSettings(int currentResolutionIndex)
    {
        if (PlayerPrefs.HasKey(RESOLUTION))
            _resolutionDropdown.value = PlayerPrefs.GetInt(RESOLUTION);
        else
            _resolutionDropdown.value = currentResolutionIndex;

        if (PlayerPrefs.HasKey(FULLSCREEN))
            Screen.fullScreen = System.Convert.ToBoolean(PlayerPrefs.GetInt(FULLSCREEN));
        else
            Screen.fullScreen = true;
    }
}
