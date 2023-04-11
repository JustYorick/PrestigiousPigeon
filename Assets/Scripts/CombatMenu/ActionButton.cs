using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform), typeof(Button))]
public class ActionButton : MonoBehaviour{
    [Header("Text")]
    [SerializeField] private TMPro.TMP_Text text;
    [SerializeField] private Vector2 narrowTextPosition;
    [SerializeField] private Vector2 wideTextPosition;
    [SerializeField] private string narrowText;
    [SerializeField] private string wideText;
    
    [Header("Button")]
    [SerializeField] private Vector2 narrowPosition;
    [SerializeField] private Vector2 widePosition;
    [SerializeField] private Vector2 narrowSize;
    [SerializeField] private Vector2 wideSize;

    [Header("Other buttons")]
    [SerializeField] private ActionButton[] buttons;

    public bool active{get; private set;} = false;

    private RectTransform rectTransform;
    
    void Awake(){
        // Retrieve the rect transform and button of the current object
        rectTransform = GetComponent<RectTransform>();
        Button button = GetComponent<Button>();

        // Add a listener for the OnClick of the button, to make the button wide
        button.onClick.AddListener(Activate);

        // Set the default values to narrow
        MakeNarrow();
    }
    
    void MakeWide(){
        // Make the other buttons narrow
        for(int i = 0;i < buttons.Length;i++){
            buttons[i].Deactivate();
        }

        // Make this button wide
        text.rectTransform.anchoredPosition = wideTextPosition;
        text.SetText(wideText);
        rectTransform.anchoredPosition = widePosition;
        rectTransform.sizeDelta = wideSize;
    }

    void MakeNarrow(){
        // Make this button narrow
        text.rectTransform.anchoredPosition = narrowTextPosition;
        text.SetText(narrowText);
        rectTransform.anchoredPosition = narrowPosition;
        rectTransform.sizeDelta = narrowSize;
    }

    private void Activate(){
        // Activate the button, if it's currently inactive
        if(!active){
            MakeWide();
            active = true;
        }
    }

    public void Deactivate(){
        // Deactivate the button, if it's currently active
        if(active){
            MakeNarrow();
            active = false;
        }
    }
}
