using ReDesign;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace CombatMenu
{
    public class SpellMenu : MonoBehaviour{
        [SerializeField] private ActionButton spellButton;
        [SerializeField] private ActionButton movementButton;
        [SerializeField] public StatusBar mana;
        [SerializeField] public int minimumMana = 2;
        [SerializeField] private KeyCode closeKeyBinding;
        private Canvas canvas;
        private GraphicRaycaster raycaster;
        private ReDesign.MouseController mouseController;
        public bool AllowedToOpen = true;
        public UnityEvent OnClose = new UnityEvent();

        public bool IsOpen => canvas.enabled;

        void Start(){
            // Retrieve the canvas component and the mouse controller
            canvas = GetComponent<Canvas>();
            raycaster = GetComponent<GraphicRaycaster>();
            raycaster.enabled = canvas.enabled;
            mouseController = GameObject.Find("MouseController").GetComponent<ReDesign.MouseController>();
        }

        void Update(){
            // Close the spell menu and deselect the selected spell on escape
            if(canvas.enabled && Input.GetKeyDown(closeKeyBinding)){
                Close();
                movementButton.Activate();
            }
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
                raycaster.enabled = true;
                RangeTileTool.Instance.clearTileMap(RangeTileTool.Instance.rangeTileMap);
            }else{
                movementButton.Activate();
            }
        }

        // Close the spell menu
        public void Close(){
            canvas.enabled = false;
            raycaster.enabled = false;
            mouseController.DeselectSpell();
            OnClose.Invoke();
            RangeTileTool.Instance.clearTileMap(mouseController.SelectorMap);
            if (StateController.currentState == GameState.PlayerTurn)
            {
                RangeTileTool.Instance.drawMoveRange(WorldController.getPlayerTile(), mana.Value);
            }
        }
    }
}
