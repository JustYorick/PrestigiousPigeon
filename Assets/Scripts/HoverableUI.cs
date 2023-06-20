 using System;
 using UnityEngine;
 using UnityEngine.EventSystems;

 public class HoverableUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
 {
     private Vector3 defaultScale;
     
     private void Start()
     {
         defaultScale = gameObject.transform.localScale;
     }

     public void OnPointerEnter(PointerEventData eventData)
     {
         gameObject.transform.localScale = defaultScale * 1.1f;
     }
 
     public void OnPointerExit(PointerEventData eventData)
     {
         gameObject.transform.localScale = defaultScale;
     }
 }