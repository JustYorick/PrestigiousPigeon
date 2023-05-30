using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
    private Button _prevLevelButton;
    private Button _nextLevelButton;
    [SerializeField] private GameObject playButton;
    private static bool _animationPlayed;
    private string _levelAt;
    private string _prevLevel;
    private string _selectedLevel;

    void Awake()
    {
        playButton.SetActive(false);
        _prevLevel = PlayerPrefs.GetString("prevLevel");
        _levelAt = PlayerPrefs.GetString("levelAt");
        // if no levels have been completed activate tutorialmap button
        if (_prevLevel.Equals(""))
        {
            CheckUnlocked("TutorialWithTerrainMap", "TutorialWithTerrainMap",
                GameObject.Find("TutorialWithTerrainMapButton").GetComponent<Button>());
        }
        // activate button for next level
        else
        {
            _nextLevelButton = GameObject.Find(_levelAt + "Button").GetComponent<Button>();
            CheckUnlocked(_levelAt, _prevLevel, _nextLevelButton);
        }
    }

    // checks if level is unlocked
    public void CheckUnlocked(string currentLevel, string prevLevel, Button nextButton)
    {
        // if the previous level is not the current level animate the unlock of the new level
        if (!prevLevel.Equals(currentLevel))
        {
            AnimateUnlock(currentLevel, prevLevel, nextButton);
        }
        // make the next level button interactable again
        nextButton.interactable = true;
    }

    // animate new level unlocking
    public static void AnimateUnlock(string currentLevel, string prevLevel, Button nextButton)
    {
        // TODO: animate unlock from previous button to next button
        Button prevButton = GameObject.Find(prevLevel + "Button").GetComponent<Button>();
        Vector3 nextButtonLoc = nextButton.transform.position;
        Vector3 prevButtonLoc = prevButton.transform.position;
        // use locations to draw arrow between
        PlayerPrefs.GetInt(currentLevel + "anim");
    }

    public void SetSelectedLevel(string level)
    {
        // on click of button, set _selectedLevel to level (add full level path in Unity editor)
        _selectedLevel = level;
        // set play button visible
        playButton.SetActive(true);
    }

    public void PlayLevel()
    {
        // split _selectedLevel, so that only the Map name gets stored
        string saveLevelAt = _selectedLevel.Split('/').Last();
        // store Map name into levelAt
        PlayerPrefs.SetString("levelAt", saveLevelAt);
        // load selected level
        SceneManager.LoadScene(_selectedLevel);
    }
}