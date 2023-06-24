using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseController : MonoBehaviour
{
    private Canvas _menu;
    private GraphicRaycaster _raycaster;
    private Canvas _spellMenu;
    private Canvas _helpMenu;
    private bool _spellMenuWasOpen = false;
    private Canvas _tutorialCanvas;
    private Canvas _settings;
    private GraphicRaycaster _settingsRaycaster;

    void Awake()
    {
        _menu = GetComponent<Canvas>();
        _raycaster = GetComponent<GraphicRaycaster>();
        _menu.enabled = false;
        _raycaster.enabled = false;
        _spellMenu = GameObject.Find("SpellMenu").GetComponent<Canvas>();
        _helpMenu = GameObject.Find("HelpScreen").GetComponent<Canvas>();
        _settings = GameObject.Find("Settings").GetComponent<Canvas>();
        _settingsRaycaster = GameObject.Find("Settings").GetComponent<GraphicRaycaster>();
        _settingsRaycaster.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !_menu.enabled && !(_spellMenu.enabled || _spellMenuWasOpen))
        {
            if (SceneManager.GetActiveScene().buildIndex == 2)
            {
                _tutorialCanvas = GameObject.Find("Tutorial").GetComponent<Canvas>();
                if (!_tutorialCanvas.enabled)
                {
                    OpenMenu();
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && _menu.enabled && !_settings.enabled)
        {
            CloseMenu();
        }
        _spellMenuWasOpen = _spellMenu.enabled;
        
        if (_menu.enabled)
        {
            _raycaster.enabled = true;
        }

        if (_settings.enabled && Input.GetKeyDown(KeyCode.Escape))
        {
            _settings.enabled = false;
            _settingsRaycaster.enabled = false;
            _raycaster.enabled = true;
        }

        if (_settings.enabled && _menu.enabled)
        {
            _raycaster.enabled = false;
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
        _helpMenu.enabled = false;
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