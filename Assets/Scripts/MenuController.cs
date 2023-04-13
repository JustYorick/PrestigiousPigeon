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

    // Enabling a canvas, opens it
    public void OpenMenu() => menu.enabled = true;

    // Disable the menu to close it again
    public void CloseMenu() => menu.enabled = false;

    // Toggle the menu between open and closed
    public void ToggleMenu() => menu.enabled = !menu.enabled;
}
