using UnityEngine;
using UnityEngine.EventSystems;

namespace CombatMenu
{
    public class SpellButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler{
        [SerializeField] private TMPro.TMP_Text descriptionText;
        [SerializeField][TextArea] private string description;
    
        public void OnPointerEnter(PointerEventData data){
            descriptionText.SetText(description);
        }

        public void OnPointerExit(PointerEventData data){
            descriptionText.SetText("Select a spell");
        }
    }
}
