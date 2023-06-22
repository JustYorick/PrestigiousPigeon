using System.Collections;
using System.Collections.Generic;
using CombatMenu;
using UnityEngine;
using UnityEngine.UI;
using ReDesign;

public class SpellSlot : MonoBehaviour{
    [SerializeField] private TMPro.TMP_Text spellNameField;
    [SerializeField] private TMPro.TMP_Text spellStatsField;
    [SerializeField] private Color lockedColor;
    [SerializeField] private Color unlockedColor;
    [SerializeField] private Color activeColor;
    [SerializeField] private KeyCode binding;
    [field:SerializeField] public bool unlocked{get; private set;} = false;
    private AttacksAndSpells boundSpell;
    public bool active{get; private set;} = false;
    
    private Image spellImage;
    private Button spellButton;
    private SpellMenu spellMenu;

    void SetSpell(){
        switch(name){
            case "SpellSlot1":
                boundSpell = new BasicFireSpell(null);
                break;
            case "SpellSlot2":
                boundSpell = new BasicIceSpell(null);
                break;
            case "SpellSlot3":
                boundSpell = new BasicWaterSpell();
                break;
        }
    }

    void Awake(){
        // Retrieve the button and image components
        spellButton = GetComponent<Button>();
        spellImage = GetComponent<Image>();

        // Find the spell menu
        spellMenu = GameObject.Find("SpellMenu").GetComponent<SpellMenu>();

        // Disable the spell slot when the spell spell menu closes
        spellMenu.OnClose.AddListener(Disable);

        // Add a listener for when the button is clicked
        spellButton.onClick.AddListener(Enable);

        // Set the color depending on whether the spell should be unlocked
        spellImage.color = unlocked? unlockedColor : lockedColor;

        // Retrieve a reference to all other spell slots and disable them when the button is clicked
        List<SpellSlot> spellSlots = new List<SpellSlot>(gameObject.transform.parent.GetComponentsInChildren<SpellSlot>());
        spellSlots.Remove(this);
        spellSlots.ForEach(slot => spellButton.onClick.AddListener(slot.Disable));

        // disable clicking locked spells, allow clicking unlocked ones
        spellButton.interactable = unlocked;
    }

    void Start(){
        if(enabled){
            SetSpell();
        }
    }

    void Update(){
        // Simulate a button click, if the button is unlocked, the keybinding is down, and the spell menu is open
        if(unlocked && Input.GetKeyDown(binding) && spellMenu.IsOpen){
            spellButton.onClick.Invoke();
        }
    }

    public void Enable(){
        // Toggle the the spell between active and inactive
        active = true;

        // Set the color, based on whether the spell should be active
        spellImage.color = activeColor;

        // Fill the spell info bar with info about the spell
        spellNameField.text = boundSpell.attackName;
        spellStatsField.text = $"Range: {boundSpell.MaximumRange}\nDamage: {boundSpell.Damage}\nMana cost: {boundSpell.ManaCost}";
        RangeTileTool.Instance.clearTileMap(MouseController.Instance.SelectorMap);
        MouseController.Instance.DrawCurrentSpellRange();
    }
    
    public void Disable(){
        // Disable the button, if it is unlocked and active
        if(active && unlocked){
            active = false;
            spellImage.color = unlockedColor;
        }
    }
}
