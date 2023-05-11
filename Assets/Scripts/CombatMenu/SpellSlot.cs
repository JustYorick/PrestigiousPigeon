using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellSlot : MonoBehaviour{
    [SerializeField] private TMPro.TMP_Text spellNameField;
    [SerializeField] private TMPro.TMP_Text spellRangeField;
    [SerializeField] private TMPro.TMP_Text spellDamageField;
    [SerializeField] private string spellName;
    [SerializeField] private int spellRange;
    [SerializeField] private int spellDamage;
    [field:SerializeField] public bool unlocked{get; private set;} = false;

    private Button spellButton;
    private Image buttonImage;

    void Awake(){
        spellButton = GetComponent<Button>();
        spellButton.onClick.AddListener(OnSelect);
        spellButton.enabled = unlocked;
        Debug.Log($"Who awoke the {spellName} button?!");
    }

    void OnSelect(){
        Debug.Log($"{spellName} Spell button clicked");
        spellNameField.text = spellName;
        spellRangeField.text = spellRange.ToString();
        spellDamageField.text = spellDamage.ToString();
    }
}
