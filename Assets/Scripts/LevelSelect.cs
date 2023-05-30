using UnityEngine;
using System.Collections.Generic;
using ReDesign.Entities;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
    private Button _prevLevelButton;
    private Button _nextLevelButton;
    private static bool _animationPlayed;
    private string _levelAt;
    private string _prevLevel;

    void Awake()
    {
        _prevLevel = PlayerPrefs.GetString("prevLevel");
        _levelAt = PlayerPrefs.GetString("levelAt");
        Debug.Log(_prevLevel);
        Debug.Log(_levelAt);
        if (_prevLevel.Equals(""))
        {
            Debug.Log("ahahhai");
            CheckUnlocked("TutorialMapWithTerrain", "TutorialMapWithTerrain",
                GameObject.Find("TutorialMapWithTerrainButton").GetComponent<Button>());
        }
        else
        {
            Debug.Log("bozo");
            _nextLevelButton = GameObject.Find(_levelAt + "Button").GetComponent<Button>();
            CheckUnlocked(_levelAt, _prevLevel, _nextLevelButton);
        }
    }

    // checks if level is unlocked
    public void CheckUnlocked(string currentLevel, string prevLevel, Button nextButton)
    {
        if (!prevLevel.Equals(currentLevel))
        {
            Debug.Log("true bro");
            AnimateUnlock(currentLevel, prevLevel, nextButton);
        }
        Debug.Log("false man");
        Debug.Log(nextButton);
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
}