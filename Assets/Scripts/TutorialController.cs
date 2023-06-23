using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour
{

    private Canvas _tutorialCanvas;
    private Canvas _statsCanvas;
    private Canvas _buttonsCanvas;
    private Canvas _spellsCanvas;
    private Canvas _infoCanvas;
    private Button _statsNext;
    private Button _buttonsNext;
    private Button _spellsNext;
    private Button _infoNext;
    private int _onClickCount;
    
    void Awake()
    {
        _statsNext = GameObject.Find("StatsNextButton").GetComponent<Button>();
        _buttonsNext = GameObject.Find("ButtonsNextButton").GetComponent<Button>();
        _spellsNext = GameObject.Find("SpellsNextButton").GetComponent<Button>();
        _infoNext = GameObject.Find("InfoNextButton").GetComponent<Button>();
        _tutorialCanvas = GameObject.Find("Tutorial").GetComponent<Canvas>();
        _statsCanvas = GameObject.Find("StatsCanvas").GetComponent<Canvas>();
        _buttonsCanvas = GameObject.Find("ButtonsCanvas").GetComponent<Canvas>();
        _spellsCanvas = GameObject.Find("SpellsCanvas").GetComponent<Canvas>();
        _infoCanvas = GameObject.Find("InfoCanvas").GetComponent<Canvas>();
    }

    void Start()
    {
        
    }
    
    void Update()
    {
        if (_tutorialCanvas.enabled)
        {
            Time.timeScale = 0;
        }
    }

    public void SkipTutorial()
    {
        _tutorialCanvas.enabled = false;
    }

    public void StatsNext()
    {
        _onClickCount++;
        if (_onClickCount == 2)
        {
            _buttonsCanvas.enabled = true;
            _onClickCount = 0;
        }
    }
    
    public void ButtonsNext()
    {
        if (_onClickCount == 3)
        {
            _spellsCanvas.enabled = true;
            _buttonsCanvas.enabled = false;
            _onClickCount = 0;
        }
    }
    
    public void SpellsNext()
    {
        if (_onClickCount == 2)
        {
            _spellsCanvas.enabled = false;
            _infoCanvas.enabled = true;
            _onClickCount = 0;
        }
    }
    
    public void InfoNext()
    {
        _tutorialCanvas.enabled = false;
        Time.timeScale = 1;
    }
}
