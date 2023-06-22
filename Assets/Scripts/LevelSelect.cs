using System.Collections.Generic;
using System.IO;
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
    private string _nextLevel;
    private string _prevLevel;
    private int _levelsBeaten;
    private string _selectedLevel;
    [SerializeField] private Button tutorialMapButton;
    [SerializeField] private Button level1MapButton;
    [SerializeField] private Button level2MapButton;
    [SerializeField] private Button level3MapButton;
    private List<Button> _buttons;

    void Awake()
    {
        _buttons = new List<Button>
        {
            tutorialMapButton,
            level1MapButton,
            level2MapButton,
            level3MapButton
        };
        
        _levelsBeaten = PlayerPrefs.GetInt("levelsBeaten");
        for (int i = 0; i < _levelsBeaten; i++)
        {
            if (i < 4)
            {
                _buttons[i].interactable = true;
            }
        }
        playButton.SetActive(false);
        _prevLevel = PlayerPrefs.GetString("prevLevel");
        // if no levels have been completed activate tutorialmap button
        if (_prevLevel.Equals(""))
        {
            CheckUnlocked("TutorialWithTerrainMap", "TutorialWithTerrainMap",
                GameObject.Find("TutorialWithTerrainMapButton").GetComponent<Button>());
        }
        // activate button for next level
        else if (_levelsBeaten < 4)
        {
            _nextLevel = SceneUtility.GetScenePathByBuildIndex(_levelsBeaten + 1);
            _nextLevel = _nextLevel.Split('/').Last();
            _nextLevel = Path.GetFileNameWithoutExtension(_nextLevel);
            _nextLevelButton = GameObject.Find(_nextLevel + "Button").GetComponent<Button>();
            CheckUnlocked(_nextLevel, _prevLevel, _nextLevelButton);
        }
    }

    // checks if level is unlocked
    public void CheckUnlocked(string nextLevel, string prevLevel, Button nextButton)
    {
        // if the previous level is not the current level animate the unlock of the new level
        if (!prevLevel.Equals(nextLevel))
        {
            AnimateUnlock(nextLevel, prevLevel, nextButton);
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
        SceneManager.LoadScene(_selectedLevel);
    }
}