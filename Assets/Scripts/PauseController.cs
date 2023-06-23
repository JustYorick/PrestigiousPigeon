using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseController : MonoBehaviour
{
    private Canvas _menu;
    private GraphicRaycaster _raycaster;
    private Canvas _spellMenu;
    private Canvas _helpMenu;
    private Canvas _tutorialCanvas;

    void Awake()
    {
        _menu = GetComponent<Canvas>();
        _raycaster = GetComponent<GraphicRaycaster>();
        _menu.enabled = false;
        _raycaster.enabled = false;
        _spellMenu = GameObject.Find("SpellMenu").GetComponent<Canvas>();
        _helpMenu = GameObject.Find("HelpScreen").GetComponent<Canvas>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !_menu.enabled && !_spellMenu.enabled)
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