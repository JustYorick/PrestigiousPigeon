using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour
{
    [SerializeField] private Canvas tutorialCanvas;
    [SerializeField] private Canvas introCanvas;
    [SerializeField] private Canvas statsCanvas;
    [SerializeField] private Canvas buttonsCanvas;
    [SerializeField] private Canvas spellsCanvas;
    [SerializeField] private Canvas infoCanvas;
    [SerializeField] private GameObject manaImages;
    [SerializeField] private GameObject healthImages;
    [SerializeField] private GameObject moveButtonImages;
    [SerializeField] private GameObject spellButtonImages;
    [SerializeField] private GameObject endTurnButtonImages;
    [SerializeField] private GameObject spellsImages;
    [SerializeField] private GameObject spellsInfoImages;
    [SerializeField] private GameObject healthText;
    [SerializeField] private GameObject manaText;
    [SerializeField] private GameObject moveButtonText;
    [SerializeField] private GameObject spellButtonText;
    [SerializeField] private GameObject endTurnButtonText;
    [SerializeField] private GameObject spellsText;
    [SerializeField] private GameObject spellsInfoText;
    [SerializeField] private Button statsNext;
    [SerializeField] private Button buttonsNext;
    [SerializeField] private Button spellsNext;
    [SerializeField] private Button infoNext;
    [SerializeField] private Button skipButton;
    [SerializeField] private Canvas spellMenu;
    private int _onClickCount;
    [SerializeField] private CollapseableUI collapseableUI;
    [SerializeField] private ActionButton moveActionButton;
    [SerializeField] private Button moveButton;
    [SerializeField] private Button spellButton;
    [SerializeField] private Button endTurnButton;

    private void Start()
    {
        statsNext.onClick.AddListener(IncreaseCount);
        buttonsNext.onClick.AddListener(IncreaseCount);
        spellsNext.onClick.AddListener(IncreaseCount);
        skipButton.onClick.AddListener(ShowObjective);
        infoNext.onClick.AddListener(ShowObjective);
        manaImages.SetActive(false);
        manaText.SetActive(false);
        spellButtonImages.SetActive(false);
        spellButtonText.SetActive(false);
        endTurnButtonImages.SetActive(false);
        endTurnButtonText.SetActive(false);
        spellsInfoImages.SetActive(false);
        spellsInfoText.SetActive(false);
        moveActionButton.Deactivate();
        moveButton.enabled = false;
        spellButton.enabled = false;
        endTurnButton.interactable = false;
    }

    public void IntroNext()
    {
        introCanvas.enabled = false;
        statsCanvas.enabled = true;
    }

    public void StatsNext()
    {
        switch (_onClickCount)
        {
            case 1:
                healthImages.SetActive(false);
                healthText.SetActive(false);
                manaImages.SetActive(true);
                manaText.SetActive(true);
                break;
            case 2:
                statsCanvas.enabled = false;
                buttonsCanvas.enabled = true;
                _onClickCount = 0;
                break;
        }
    }
    
    public void ButtonsNext()
    {
        switch (_onClickCount)
        {
            case 1:
                moveButtonImages.SetActive(false);
                moveButtonText.SetActive(false);
                spellButtonImages.SetActive(true);
                spellButtonText.SetActive(true);
                break;
            case 2:
                spellButtonImages.SetActive(false);
                spellButtonText.SetActive(false);
                endTurnButtonImages.SetActive(true);
                endTurnButtonText.SetActive(true);
                break;
            case 3:
                spellsCanvas.enabled = true;
                buttonsCanvas.enabled = false;
                spellMenu.enabled = true;
                _onClickCount = 0;
                break;
        }
    }
    
    public void SpellsNext()
    {
        switch (_onClickCount)
        {
            case 1:
                spellsImages.SetActive(false);
                spellsText.SetActive(false);
                spellsInfoImages.SetActive(true);
                spellsInfoText.SetActive(true);
                break;
            case 2:
                spellsCanvas.enabled = false;
                infoCanvas.enabled = true;
                _onClickCount = 0;
                spellMenu.enabled = false;
                break;
        }
    }
    
    public void InfoNext()
    {
        tutorialCanvas.enabled = false;
        moveActionButton.Activate();
        moveButton.enabled = true;
        spellButton.enabled = true;
        infoCanvas.enabled = false;
        endTurnButton.interactable = true;
        skipButton.interactable = false;
        infoNext.interactable = false;
    }

    public void IncreaseCount()
    {
        _onClickCount++;
    }

    public void ShowObjective()
    {
        collapseableUI.ShowObjectiveUI();
    }
}
