using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellMenu : MonoBehaviour{
    private Canvas canvas;
    [SerializeField] private ActionButton spellButton;
    [SerializeField] private StatusBar mana;
    [SerializeField] private int minimumMana = 2;

    // Retrieve the canvas component
    void Start() => canvas = GetComponent<Canvas>();
    
    public void Open(){
        // Only open the spell menu, if the player has enough mana
        if(mana.Value >= minimumMana){
            canvas.enabled = true;
            spellButton.Deactivate();
        }
    }

    // Close the spell menu
    public void Close() => canvas.enabled = false;
}
