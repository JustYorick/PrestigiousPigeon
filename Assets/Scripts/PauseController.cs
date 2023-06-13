using UnityEngine;
using UnityEngine.UI;

public class PauseController : MonoBehaviour
{
    private Canvas _menu;
    private GraphicRaycaster _raycaster;
    private Canvas spellMenu;
        
    void Awake()
    {
        _menu = GetComponent<Canvas>();
        _raycaster = GetComponent<GraphicRaycaster>();
        _menu.enabled = false;
        _raycaster.enabled = false;
        spellMenu = GameObject.Find("SpellMenu").GetComponent<Canvas>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !_menu.enabled && !spellMenu.enabled)
        {
            OpenMenu();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && _menu.enabled)
        {
            CloseMenu();
        }
    }

    void OpenMenu(){
        _menu.enabled = true;
        _raycaster.enabled = true;
        Time.timeScale = 0;
    }

    public void CloseMenu()
    {
        Time.timeScale = 1;
        _menu.enabled = false;
        _raycaster.enabled = false;
    }
}