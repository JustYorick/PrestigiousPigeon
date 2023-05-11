using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellSlot : MonoBehaviour{
    [SerializeField] private TMPro.TMP_Text spellNameField;
    [SerializeField] private TMPro.TMP_Text rangeField;
    [SerializeField] private TMPro.TMP_Text damageField;
    [SerializeField] private string spellName;
    [SerializeField] private int range = 0;
    [SerializeField] private int damage = 0;
    [SerializeField] private Color lockedColor;
    [SerializeField] private Color unlockedColor;
    [SerializeField] private Color activeColor;
    [field:SerializeField] public bool unlocked{get; private set;} = false;
    public bool active{get; private set;} = false;
    
    private Button spellButton;
    private Image spellImage;

    void Awake(){
        // Retrieve the button and image components
        spellButton = GetComponent<Button>();
        spellImage = GetComponent<Image>();

        // Add a listener for when the button is clicked
        spellButton.onClick.AddListener(OnClick);

        // disable clicking locked spells, allow clicking unlocked ones
        spellButton.enabled = unlocked;

        // Set the color depending on whether the spell should be unlocked
        spellImage.color = unlocked? unlockedColor : lockedColor;
    }

    void OnClick(){
        // Toggle the the spell between active and inactive
        active = !active;

        // Set the color, based on whether the spell should be active
        spellImage.color = active? activeColor : unlockedColor;

        // Fill the spell info bar with info about the spell
        spellNameField.text = spellName;
        rangeField.text = range.ToString();
        damageField.text = damage.ToString();
    }
}
