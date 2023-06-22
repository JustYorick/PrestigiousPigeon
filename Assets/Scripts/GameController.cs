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
    public void Awake()
    {
        Debug.Log(PlayerPrefs.GetInt("cutsceneSave"));
        ApplySettings();
    }

    public void LoadScene(string sceneName){
        // Make sure the game controller won't be destroyed
        DontDestroyOnLoad(gameObject);


        Time.timeScale = 1;
        // Load the scene
        SceneManager.LoadScene(sceneName);
    }

    // Reload the active scene
    public void ReloadCurrentScene() => LoadScene(SceneManager.GetActiveScene().name);

    private void LoadVolume(){

        // Set the sound settings to the stored sound settings, if the sound setting is on
        musicVolume = PlayerPrefs.GetFloat("MusicVolume");
        effectVolume = PlayerPrefs.GetFloat("EffectVolume");
        
        SoundManager.Instance.SetEffectsVolume(effectVolume);
        SoundManager.Instance.SetMusicVolume(musicVolume);
    }
    
    public void DeleteSave()
    {
        PlayerPrefs.DeleteKey("prevLevel");
        PlayerPrefs.DeleteKey("levelsBeaten");
        PlayerPrefs.DeleteKey("cutsceneSave");
    }
    
    public void ContinueLevel()
    {
        int chapter = PlayerPrefs.GetInt("cutsceneSave");

        // save the current scene as the previously beaten level
        PlayerPrefs.SetString("prevLevel", SceneManager.GetActiveScene().name);
        PlayerPrefs.SetInt("levelsBeaten", SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene("Ch" + chapter + "_PostCombat");
        if (SceneManager.GetActiveScene().buildIndex - 2 != PlayerPrefs.GetInt("levelsBeaten"))
        {
            chapter++;
            PlayerPrefs.SetInt("cutsceneSave", chapter);
        }
    }
}
