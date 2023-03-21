using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    Toggle toggle;
    Slider slider;
    [SerializeField] string fieldName;

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

    void SaveSetting(string name, bool value){
        // Save the setting as an integer with the value 0 or 1
        PlayerPrefs.SetInt(name, value? 1: 0);
    }

    void SaveSetting(string name, float value){
        // Save the setting as a float
        PlayerPrefs.SetFloat(name, value);
    }

    public static bool ReadSettingBool(string name){
        // Read the last saved setting, return 1 if there is no setting with this name available
        return PlayerPrefs.GetInt(name, 1) >= 1;
    }

    public static float ReadSettingFloat(string name){
        // Read the last saved setting, return 0 if there is no setting with this name available
        return PlayerPrefs.GetFloat(name, 0.0f);
    }
}
