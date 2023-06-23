using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour
{

    private Canvas _tutorialCanvas;
    private Canvas _introCanvas;
    private Canvas _statsCanvas;
    private Canvas _buttonsCanvas;
    private Canvas _spellsCanvas;
    private Canvas _infoCanvas;
    private int _onClickCount;
    private GameObject _healthImages;
    private GameObject _manaImages;
    private GameObject _moveButtonImages;
    private GameObject _spellButtonImages;
    private GameObject _endTurnButtonImages;
    private GameObject _spellsImages;
    private GameObject _spellsInfoImages;
    private GameObject _healthText;
    private GameObject _manaText;

    void Awake()
    {
        _tutorialCanvas = GameObject.Find("Tutorial").GetComponent<Canvas>();
        _introCanvas = GameObject.Find("IntroCanvas").GetComponent<Canvas>();
        _statsCanvas = GameObject.Find("StatsCanvas").GetComponent<Canvas>();
        _buttonsCanvas = GameObject.Find("ButtonsCanvas").GetComponent<Canvas>();
        _spellsCanvas = GameObject.Find("SpellsCanvas").GetComponent<Canvas>();
        _infoCanvas = GameObject.Find("InfoCanvas").GetComponent<Canvas>();
        _healthImages = GameObject.Find("HealthImages");
        _manaImages = GameObject.Find("ManaImages");
        _moveButtonImages = GameObject.Find("MoveButtonImages");
        _spellButtonImages = GameObject.Find("SpellButtonImages");
        _endTurnButtonImages = GameObject.Find("EndTurnButtonImages");
        _spellsImages = GameObject.Find("SpellsImages");
        _spellsInfoImages = GameObject.Find("SpellsInfoImages");
        _healthText = GameObject.Find("HealthText");
        _manaText = GameObject.Find("ManaText");
        _manaImages.SetActive(false);
        _manaText.SetActive(false);
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

    public void IntroNext()
    {
        _introCanvas.enabled = false;
        _statsCanvas.enabled = true;
    }

    public void StatsNext()
    {
        _onClickCount++;
        switch (_onClickCount)
        {
            case 1:
                _healthImages.SetActive(false);
                _manaImages.SetActive(true);
                _manaText.SetActive(true);
                break;
            case 2:
                _statsCanvas.enabled = false;
                _buttonsCanvas.enabled = true;
                _onClickCount = 0;
                break;
        }
    }
    
    public void ButtonsNext()
    {
        _onClickCount++;
        switch (_onClickCount)
        {
            case 1:
                _moveButtonImages.SetActive(false);
                _moveButtonText.SetActive(false);
                _spellButtonImages.SetActive(true);
                _spellButtonText.SetActive(true);
                break;
            case 2:
                _spellButtonImages.SetActive(false);
                _spellButtonText.SetActive(false);
                _endTurnButtonImages.SetActive(true);
                _endTurnButtonText.SetActive(true);
                break;
            case 3:
                _spellsCanvas.enabled = true;
                _buttonsCanvas.enabled = false;
                _onClickCount = 0;
                break;
        }
    }
    
    public void SpellsNext()
    {
        _onClickCount++;
        switch (_onClickCount)
        {
            case 1:
                _spellsImages.SetActive(false);
                _spellsInfoImages.SetActive(true);
                break;
            case 2:
                _spellsCanvas.enabled = false;
                _infoCanvas.enabled = true;
                _onClickCount = 0;
                break;
        }
    }
    
    public void InfoNext()
    {
        _tutorialCanvas.enabled = false;
        Time.timeScale = 1;
    }
}
