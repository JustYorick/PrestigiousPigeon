using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform), typeof(Button))]
public class ActionButton : MonoBehaviour{
    [Header("Narrow Button")]
    [SerializeField] private Vector2 narrowPosition;
    [SerializeField] private Vector2 narrowSize;
    [SerializeField] private Sprite narrowTexture;

    [Header("Wide Button")]
    [SerializeField] private Vector2 widePosition;
    [SerializeField] private Vector2 wideSize;
    [SerializeField] private Sprite wideTexture;

    [Header("Other buttons")]
    [SerializeField] private ActionButton[] buttons;

    [field:SerializeField] public bool active{get; private set;} = false;
    [field:SerializeField] private KeyCode keyBinding;
    private Image image;
    private Button button;
    private RectTransform rectTransform;
    
    void Start(){
        // Retrieve the rect transform and button of the current object
        rectTransform = GetComponent<RectTransform>();
        image = GetComponent<Image>();
        button = GetComponent<Button>();

        // Add a listener for the OnClick of the button, to make the button wide
        button.onClick.AddListener(Activate);

        // Make the button wide or narrow depending on whether the button is active
        if(active){
            MakeWide();
        }else{
            MakeNarrow();
        }
    }

    void Update(){
        if(Input.GetKeyDown(keyBinding)){
            button.onClick.Invoke();
        }
    }

    void MakeNarrow(){
        // Make this button narrow
        rectTransform.anchoredPosition = narrowPosition;
        rectTransform.sizeDelta = narrowSize;

        // Set the correct image
        image.sprite = narrowTexture;
    }

    void MakeWide(){
        // Make the other buttons narrow
        for(int i = 0;i < buttons.Length;i++){
            buttons[i].Deactivate();
        }

        // Make this button wide
        rectTransform.anchoredPosition = widePosition;
        rectTransform.sizeDelta = wideSize;

        // Set the correct image
        image.sprite = wideTexture;
    }

    void Activate(){
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

    void OnClick(){}
}
