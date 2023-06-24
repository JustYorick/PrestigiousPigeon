using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    Toggle toggle;
    Slider slider;
    [SerializeField] string fieldName;
    // To play when slider changes
    [SerializeField]private AudioClip sliderSound;


    private void Awake()
    {
        //SaveSetting("Sound", false);
        //PlayerPrefs.SetInt("Sound", 0);
        if (PlayerPrefs.GetFloat("EffectVolume") == 0f)
        {
            PlayerPrefs.SetFloat("EffectVolume", 0.2f);
        }
        if (PlayerPrefs.GetFloat("MusicVolume") == 0f)
        {
            PlayerPrefs.SetFloat("MusicVolume", 0.2f);
        }
    }

    // Start is called before the first frame update
    void Start(){
        // Load the toggle or slider of the containing game object
        toggle = GetComponent<Toggle>();
        slider = GetComponent<Slider>();

        // Set the value of the toggle or slider to the saved value
        if(toggle != null){
            toggle.isOn = ReadSettingBool(fieldName);
        }else if(slider != null){
            slider.value = ReadSettingFloat(fieldName);
        }
    }

    public void SaveSetting(){
        // Save the value of the toggle or slider with the name of the field
        if(toggle != null){
            SaveSetting(fieldName, toggle.isOn);
        }else if(slider != null){
            SaveSetting(fieldName, slider.value);
        }
    }

    public void ResetInput(){
        // Reset the toggle or slider to the last value saved
        if(toggle != null){
            toggle.isOn = ReadSettingBool(fieldName);
        }else if(slider != null){
            slider.value = ReadSettingFloat(fieldName);
        }
    }

    // Save the setting as an integer with the value 0 or 1
    void SaveSetting(string name, bool value) => PlayerPrefs.SetInt(name, value? 1: 0);

    // Save the setting as a float
    void SaveSetting(string name, float value) => PlayerPrefs.SetFloat(name, value);

    // Read the last saved setting, return 1 if there is no setting with this name available
    public static bool ReadSettingBool(string name) => PlayerPrefs.GetInt(name, 1) == 1;

    // Read the last saved setting, return 0 if there is no setting with this name available
    public static float ReadSettingFloat(string name) => PlayerPrefs.GetFloat(name, 0.2f);

    public void OnEndDrag()
    {
        SoundManager.Instance.PlaySoundWithVolume(sliderSound,slider.value);
    }

    public void ToggleAudio()
    {
        if (PlayerPrefs.GetInt("Sound") == 0)
        {
            PlayerPrefs.SetInt("Sound", 1);
        }
        else
        {
            PlayerPrefs.SetInt("Sound", 0);
        }
        SoundManager.Instance.ToggleAllSounds();
    }

    public void ToggleFullscreen() => Screen.fullScreen = !Screen.fullScreen;
}
