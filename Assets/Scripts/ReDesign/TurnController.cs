using ReDesign.Entities;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


namespace ReDesign
{
    public class TurnController : MonoBehaviour
    {
        private static TurnController _instance;

        public static TurnController Instance
        {
            get { return _instance; }
        }

        [SerializeField] private GameObject levelUI;
        public static int TurnCount = 0;
        public static int _turnPart = 0;
        public static bool gameOver = false;
        private static List<Entity> _entities = new List<Entity>();
        private static Canvas _gameOver;
        private static RawImage _controlsPanel;
        public UnityEvent gameOverEvent = new UnityEvent();
        private static CollapseableUI collapseableUI;
        private static GameObject _retryButton;
        private static GameObject _continueButton;
        private static TextMeshProUGUI _gameOverText;
        private static int _chapter;

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                _instance = this;
            }

            _turnPart = 0;
            TurnCount = 0;
            gameOver = false;
            _gameOver = GameObject.Find("GameOver").GetComponent<Canvas>();
            _gameOver.enabled = false;
            _gameOverText = _gameOver.gameObject.GetComponentInChildren<TextMeshProUGUI>();
            _retryButton = GameObject.Find("RetryButton");
            _retryButton.SetActive(true);
            _continueButton = GameObject.Find("ContinueButton");
            _continueButton.SetActive(false);
        }

        private void Start()
        {
            collapseableUI = levelUI.GetComponent<CollapseableUI>();
            gameOverEvent.AddListener(showGameOver);
            FillEntityList();
        }

        public static void ResolveNextTurn()
        {
            FillEntityList();
            showGameOver();
            if (_turnPart < _entities.Count && !gameOver)
            {
                if (_turnPart == 0)
                {
                    collapseableUI?.ShowPlayerTurnUI(); 
                }
                if (_turnPart == 1)
                {
                    collapseableUI?.ShowEnemyTurnUI();
                }
                
                
                _entities[_turnPart].NextAction();
                _turnPart++;
            }

            if (_turnPart >= _entities.Count)
            {
                _turnPart = 0;
                TurnCount++;
            }
        }

        public static void FillEntityList()
        {
            _entities = WorldController.getEntities();
        }

        public void RemoveUI()
        {
            _gameOver.enabled = false;
        }

        private static void showGameOver()
        {
            Scene currentScene = SceneManager.GetActiveScene();
            string sceneName = currentScene.name;

            switch (sceneName)
            {
                case "TutorialWithTerrainMap":
                    if (WorldController.getEntities().Where(e => e.name.Contains("Player")).Count() == 1 &&
                        WorldController.getEntities().Where(e => e.tag.Contains("Entity")).Count() == 1)
                    {
                        ChangeGameOverUI("You beat the Tutorial!");
                        _chapter = 0;
                    }

                    break;
                case "Level1Map":
                    if ((int)WorldController.getPlayerTile().XPos == 0 &&
                        (int)WorldController.getPlayerTile().YPos == 25)
                    {
                        ChangeGameOverUI("You beat Level 1!");
                        _retryButton.SetActive(false);
                        _continueButton.SetActive(true);
                        gameOver = true;
                        _chapter = 1;
                    }

                    break;
                case "Level2Map":
                    if (WorldController.getEntities().Where(e => e.name.ToLower().Contains("snow")).Count() == 0 && GameObject.Find("SnowKingAwakenTrigger").GetComponent<SnowKingAwake>().AllPillarsDestroyed)
                    { 
                        ChangeGameOverUI("You beat Level 2!");
                        _retryButton.SetActive(false);
                        _continueButton.SetActive(true);
                        gameOver = true;
                        _chapter = 2;
                    }

                    break;
                case "Level3Map":
                    if(TurnCount > 15){
                        ChangeGameOverUI("You beat Level 3!");
                        _retryButton.SetActive(false);
                        _continueButton.SetActive(true);
                        gameOver = true;
                        _chapter = 3;
                    }
                    
                    break;
            }

            if (gameOver)
            {
                _gameOver.enabled = true;
            }
        }

        private static void ChangeGameOverUI(string gameOverText)
        {
            _gameOverText.text = gameOverText;
            _retryButton.SetActive(false);
            _continueButton.SetActive(true);
            gameOver = true;
        }
        
        public void ContinueLevel()
        {
            // save the current scene as the previously beaten level
            PlayerPrefs.SetString("prevLevel", SceneManager.GetActiveScene().name);
            PlayerPrefs.SetInt("levelsBeaten", SceneManager.GetActiveScene().buildIndex);
            SceneManager.LoadScene("Ch" + _chapter + "_PostCombat");
        }
    }
}