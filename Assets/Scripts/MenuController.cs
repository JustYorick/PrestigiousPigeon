using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour {
    private Canvas menu;

    // Start is called before the first frame update
    void Start() {
        menu = gameObject.GetComponent<Canvas>();
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void OpenMenu(){
        menu.enabled = true;
    }

    public void CloseMenu(){
        menu.enabled = false;
    }
}