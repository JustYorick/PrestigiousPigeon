using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppController : MonoBehaviour{
    public void QuitGame(int code){
        // Quit the application with the given exit code, 0 = success
        Application.Quit(code);
    }

    public void ApplySettings(){
        // For now, we only need to save whether the game should be fullscreen or windowed
        Screen.fullScreen = SettingsController.ReadSettingBool("FullScreen");
    }

    // Apply the settings
    public void Start() => ApplySettings();
}
