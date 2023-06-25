using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas))]
public class MenuController : MonoBehaviour {
    private Canvas menu;
    private GraphicRaycaster raycaster;
    [SerializeField] private AudioClip menuClip;

    // Start is called before the first frame update
    void Start()
    {
        if(menuClip) SoundManager.Instance.SetMusic(menuClip);
        // Get the canvas component from the containing game object
        menu = GetComponent<Canvas>();
        raycaster = GetComponent<GraphicRaycaster>();
        raycaster.enabled = menu.enabled;
    }

    // Enabling a canvas, opens it
    public void OpenMenu(){
        menu.enabled = true;
        raycaster.enabled = true;
    }

    // Disable the menu to close it again
    public void CloseMenu()
    {
        SoundManager.Instance.SetMusicVolume(PlayerPrefs.GetFloat("MusicVolume"));
        SoundManager.Instance.SetEffectsVolume(PlayerPrefs.GetFloat("EffectVolume"));
        menu.enabled = false;
        raycaster.enabled = false;
    }

    // Toggle the menu between open and closed
    public void ToggleMenu(){
        menu.enabled = !menu.enabled;
        raycaster.enabled = !raycaster.enabled;
    }
}
