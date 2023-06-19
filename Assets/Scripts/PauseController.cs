using UnityEngine;

public class PauseController : MonoBehaviour
{
    private Canvas _menu;
    private Canvas _spellMenu;
    private Canvas _helpMenu;


    void Awake()
    {
        _menu = gameObject.GetComponent<Canvas>();
        _menu.enabled = false;
        _spellMenu = GameObject.Find("SpellMenu").GetComponent<Canvas>();
        _helpMenu = GameObject.Find("HelpScreen").GetComponent<Canvas>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !_menu.enabled && !_spellMenu.enabled)
        {
            _menu.enabled = true;
            Time.timeScale = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && _menu.enabled)
        {
            _menu.enabled = false;
            _helpMenu.enabled = false;
            Time.timeScale = 1;
        }
    }

    public void CloseMenu()
    {
        Time.timeScale = 1;
        _menu.enabled = false;
    }

    public void HideMenu()
    {
        _menu.enabled = false;
    }

    public void ViewMenu()
    {
        _menu.enabled = true;
    }
}