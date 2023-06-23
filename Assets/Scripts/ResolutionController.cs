using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResolutionController : MonoBehaviour
{
    private Resolution[] _resolutions;
    public TMP_Dropdown resolutionDropdown;
    private int _saveResolutionIndex;
    
    void Start()
    {
        _resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < _resolutions.Length; i++)
        {
            string option = _resolutions[i].width + " x " + _resolutions[i].height + " @ " +
                            _resolutions[i].refreshRate + "hz";
            options.Add(option);

            if (_resolutions[i].width == Screen.currentResolution.width &&
                _resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
        _saveResolutionIndex = currentResolutionIndex;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = _resolutions[resolutionIndex];
        _saveResolutionIndex = resolutionIndex;
        Debug.Log("set " + _saveResolutionIndex);
        Screen.SetResolution(resolution.width, resolution.height, SettingsController.ReadSettingBool("FullScreen"));
    }

    public void SaveResolution()
    {
        Resolution resolution = _resolutions[_saveResolutionIndex];
        Debug.Log("save " + _saveResolutionIndex);
        resolutionDropdown.value = _saveResolutionIndex;
        resolutionDropdown.RefreshShownValue();
        PlayerPrefs.SetInt("ResolutionWidth", resolution.width);
        PlayerPrefs.SetInt("ResolutionHeight", resolution.height);
        PlayerPrefs.SetInt("ResolutionIndex", _saveResolutionIndex);
        Debug.Log(PlayerPrefs.GetInt("ResolutionWidth") + " " + PlayerPrefs.GetInt("ResolutionHeight"));
    }

    public void ResetResolution()
    {
        Debug.Log("reset " + PlayerPrefs.GetInt("ResolutionIndex"));
        resolutionDropdown.value =  PlayerPrefs.GetInt("ResolutionIndex");
        resolutionDropdown.RefreshShownValue();
        Screen.SetResolution(PlayerPrefs.GetInt("ResolutionWidth"), PlayerPrefs.GetInt("ResolutionHeight"), SettingsController.ReadSettingBool("FullScreen"));

    }
}