using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class StatusBar : MonoBehaviour{
    [SerializeField] public int maxValue = 1;
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
    }

    void UpdateStatus(int value){
        // Make sure the status stays in bounds
        status = Mathf.Clamp(value, 0, maxValue);

        // Update the status bar
        rectTransform.sizeDelta += (maxWidth / maxValue * status - rectTransform.rect.width) * Vector2.right;
    }

    public void Fill(){
        Value = maxValue;
    }
}
