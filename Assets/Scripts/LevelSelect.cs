using UnityEngine;
using System.Collections.Generic;
using ReDesign.Entities;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
    private Button _prevLevelButton;
    private Button _nextLevelButton;
    private static bool _animationPlayed;
    private List<bool> _unlocks;
    private string _levelAt;
    private string _prevLevel;

    void Awake()
    {
        _prevLevel = PlayerPrefs.GetString("prevLevel");
        _levelAt = PlayerPrefs.GetString("levelAt");
        _nextLevelButton = GameObject.Find(_levelAt + "Button").GetComponent<Button>();
        CheckUnlocked(_levelAt, _prevLevel, _nextLevelButton);
    }

    // checks if level is unlocked
    public void CheckUnlocked(string currentLevel, string prevLevel, Button nextButton)
    {
        if (!prevLevel.Equals(currentLevel))
        {
            AnimateUnlock(currentLevel, prevLevel, nextButton);
        }
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
        nextButton.enabled = true;
    }
}