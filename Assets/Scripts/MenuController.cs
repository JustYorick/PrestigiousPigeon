using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class MenuController : MonoBehaviour {
    private Canvas menu;

    // Start is called before the first frame update
    void Start() {
        // Get the canvas component from the containing game object
        menu = gameObject.GetComponent<Canvas>();
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void OpenMenu(){
        // Enabling a canvas, opens it
        menu.enabled = true;
    }

    public void CloseMenu(){
        // Disable the menu to close it again
        menu.enabled = false;
    }
}
