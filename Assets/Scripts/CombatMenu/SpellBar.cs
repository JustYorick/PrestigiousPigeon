using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpellBar : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler{
    public bool MouseOver{get; private set;} = false;

    public void OnPointerEnter(PointerEventData eventData) => MouseOver = true;
    public void OnPointerExit(PointerEventData eventData) => MouseOver = false;
}
