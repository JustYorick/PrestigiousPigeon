using System.Collections;
using System.Collections.Generic;
using ReDesign;
using UnityEngine;
using UnityEngine.Events;
using ReDesign;

public class SpellMenu : MonoBehaviour{
    [SerializeField] private ActionButton spellButton;
    [SerializeField] private ActionButton movementButton;
    [SerializeField] private StatusBar mana;
    [SerializeField] private int minimumMana = 2;
    [SerializeField] private KeyCode closeKeyBinding;
    private Canvas canvas;
    private ReDesign.MouseController mouseController;
    public bool AllowedToOpen = true;
    public UnityEvent OnClose = new UnityEvent();

    public bool IsOpen => canvas.enabled;

    void Start(){
        // Retrieve the canvas component and the mouse controller
        canvas = GetComponent<Canvas>();
        mouseController = GameObject.Find("MouseController").GetComponent<ReDesign.MouseController>();
    }

    void LateUpdate(){
        // Close the spell menu and deselect the selected spell on escape
        if(canvas.enabled && Input.GetKeyDown(closeKeyBinding)){
            Close();
            RangeTileTool.Instance.drawMoveRange(WorldController.getPlayerTile(), mana.Value);
            movementButton.Activate();
        }
    }

    public void Open(){
        // Only open the spell menu, if the player has enough mana
        if(mana.Value >= minimumMana && AllowedToOpen){
            canvas.enabled = true;
            RangeTileTool.Instance.clearTileMap(RangeTileTool.Instance.rangeTileMap);
        }else{
            movementButton.Activate();
        }
    }

    // Close the spell menu
    public void Close(){
        canvas.enabled = false;
        mouseController.DeselectSpell();
        OnClose.Invoke();
        RangeTileTool.Instance.clearTileMap(mouseController.SelectorMap);
        if (StateController.currentState == GameState.PlayerTurn)
        {
            RangeTileTool.Instance.drawMoveRange(WorldController.getPlayerTile(), mana.Value);
        }
    }
}
