using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour{
    public float musicVolume { get; private set; }
    public float effectVolume { get; private set; }

    // Quit the application with the given exit code, 0 = success
    public void QuitGame(int code) => Application.Quit(code);

    public void ApplySettings(){
        // Make the game fullscreen or windowed, depending on the FullScreen setting
        Screen.fullScreen = SettingsController.ReadSettingBool("FullScreen");

        // Load and store the volume settings
        LoadVolume();
    }

    // Apply the settings before anything else runs
    public void Awake() => ApplySettings();

    public void LoadScene(string sceneName){
        // Make sure the game controller won't be destroyed
        DontDestroyOnLoad(gameObject);

        // Load the scene
        SceneManager.LoadScene(sceneName);
    }

    // Reload the active scene
    public void ReloadCurrentScene() => LoadScene(SceneManager.GetActiveScene().name);

    private void LoadVolume(){
        // Set all sound settings to 0, if the sound setting is off
        if(PlayerPrefs.GetInt("Sound") == 0){
            musicVolume = 0;
            effectVolume = 0;
        }else{
            // Set the sound settings to the stored sound settings, if the sound setting is on
            musicVolume = PlayerPrefs.GetFloat("MusicVolume");
            effectVolume = PlayerPrefs.GetFloat("EffectVolume");
        }
    }
    
    public void DeleteSave()
    {
        PlayerPrefs.DeleteKey("prevLevel");
        PlayerPrefs.DeleteKey("levelsBeaten");
    }
    
    public void ContinueLevel()
    {
        // save the current scene as the previously beaten level
        PlayerPrefs.SetString("prevLevel", SceneManager.GetActiveScene().name);
        PlayerPrefs.SetInt("levelsBeaten", SceneManager.GetActiveScene().buildIndex);

        SceneManager.LoadScene("LevelSelect");
    }
}
