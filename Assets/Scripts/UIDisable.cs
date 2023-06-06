using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIDisable : MonoBehaviour
{
    [SerializeField] private ManaSystem _manaSystem;
    [SerializeField] private int GrayWhenBelow;
    private Button button;
    private ActionButton actionButton;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        actionButton = GetComponent<ActionButton>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_manaSystem.GetMana() < GrayWhenBelow) 
        {
            button.interactable = false;
            actionButton.Deactivate();
        }
        else
        {
            button.interactable = true;
        }
    }
}
