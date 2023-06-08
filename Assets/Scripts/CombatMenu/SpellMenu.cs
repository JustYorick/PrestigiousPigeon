using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellMenu : MonoBehaviour{
    [SerializeField] private ActionButton spellButton;
    [SerializeField] private ActionButton movementButton;
    [SerializeField] private StatusBar mana;
    [SerializeField] private int minimumMana = 2;
    [SerializeField] private KeyCode closeKeyBinding;
    private Canvas canvas;
    private ReDesign.MouseController mouseController;

    public bool IsOpen => canvas.enabled;

    void Start(){
        // Retrieve the canvas component and the mouse controller
        canvas = GetComponent<Canvas>();
        mouseController = GameObject.Find("MouseController").GetComponent<ReDesign.MouseController>();
    }

    void Update(){
        // Close the spell menu and deselect the selected spell on escape
        if(canvas.enabled && Input.GetKeyDown(closeKeyBinding)){
            Close();
            movementButton.Activate();
        }
    }

    public void Open(){
        // Only open the spell menu, if the player has enough mana
        if(mana.Value >= minimumMana){
            canvas.enabled = true;
        }else{
            movementButton.Activate();
        }
    }

    // Close the spell menu
    public void Close(){
        canvas.enabled = false;
        mouseController.DeselectSpell();
    }

    public void OpenIfActivated(){
        if(spellButton.active){
            Open();
        }
    }
}
