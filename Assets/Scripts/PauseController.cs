using UnityEngine;

public class PauseController : MonoBehaviour
{
    private Canvas _menu;
    private Canvas spellMenu;
        
    void Awake()
    {
        _menu = gameObject.GetComponent<Canvas>();
        _menu.enabled = false;
        spellMenu = GameObject.Find("SpellMenu").GetComponent<Canvas>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !_menu.enabled && !spellMenu.enabled)
        {
            _menu.enabled = true;
            Time.timeScale = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && _menu.enabled)
        {
            _menu.enabled = false;
            Time.timeScale = 1;
        }
    }

    public void CloseMenu()
    {
        Time.timeScale = 1;
        _menu.enabled = false;
    }
}