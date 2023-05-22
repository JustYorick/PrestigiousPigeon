using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class StatusBar : MonoBehaviour{
    [SerializeField] private int maxValue = 1;
    [field:SerializeField] private int status = 1;
    [SerializeField] private float maxWidth;
    private RectTransform rectTransform;

    // External interface to the status value
    public int Value{
        get{ return status; }
        set{ UpdateStatus(value); }
    }

    void Awake(){
        // Get a reference to the RectTransform
        rectTransform = GetComponent<RectTransform>();

        // Update the statusbar
        UpdateStatus(status);

        // Decreases the value of the status bar
        StartCoroutine(LoseStats());
    }

    void UpdateStatus(int value){
        // Make sure the status stays in bounds
        status = Mathf.Clamp(value, 0, maxValue);

        // Update the status bar
        rectTransform.sizeDelta += (maxWidth / maxValue * status - rectTransform.rect.width) * Vector2.right;
    }

    // TEST FUNCTION!
    IEnumerator LoseStats(){
        // Decrement the value of the status bar every second until it's 0
        while(Value > 0){
            yield return new WaitForSeconds(1);
            Value--;
        }
    }
}
