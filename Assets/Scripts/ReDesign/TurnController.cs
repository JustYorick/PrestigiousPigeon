using ReDesign.Entities;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace ReDesign
{
    public class TurnController : MonoBehaviour
    {
        private static TurnController _instance;

        public static TurnController Instance
        {
            get { return _instance; }
        }

        public static int TurnCount = 0;
        private static int _turnPart = 0;
        public static bool gameOver = false;
        private static bool _controlsHidden;
        private static List<Entity> _entities = new List<Entity>();
        private static GameObject _gameOver;
        private static GameObject _controlsPanel;
        public UnityEvent gameOverEvent = new UnityEvent();

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

            TurnCount = 0;
            _turnPart = 0;

            gameOver = false;
            _gameOver = GameObject.Find("GameOver");
            _gameOver.SetActive(false);
            _controlsPanel = GameObject.Find("Controls");
            _controlsPanel.SetActive(false);
            _controlsHidden = true;
        }

        private void Start()
        {
            gameOverEvent.AddListener(showGameOver);
            FillEntityList();

            //starts the first turn loop
            ResolveNextTurn();
        }

        public static void ResolveNextTurn()
        {
            FillEntityList();
            showGameOver();
            if (_turnPart < _entities.Count && !gameOver)
            {
                Debug.Log("turnpart:" + _turnPart);
                _entities[_turnPart].NextAction();
                _turnPart++;
            }

            if (_turnPart >= _entities.Count)
            {
                _turnPart = 0;
                Debug.Log("all turn parts completed");
            }

            TurnCount++;
        }

        public static void FillEntityList()
        {
            _entities = WorldController.getEntities();
            //Debug.Log("entities count: "+_entities.Count);
            //Debug.Log("obst count : " + WorldController.ObstacleLayer.Count);
        }

        public void RemoveUI()
        {
            _gameOver = GameObject.Find("GameOver");
            _gameOver.SetActive(false);
        }

        public void ShowControls()
        {
            if (_controlsHidden)
            {
                _controlsPanel.SetActive(true);
                _controlsHidden = false;
            }
            else
            {
                _controlsPanel.SetActive(false);
                _controlsHidden = true;
            }
        }
        private static void showGameOver()
        {
            Scene currentScene = SceneManager.GetActiveScene();
            string sceneName = currentScene.name;
            
            switch(sceneName){
                case "TutorialMap":
                    if (WorldController.getEntities().Where(e => e.name.Contains("Player")).Count() == 1 && WorldController.getEntities().Where(e => e.tag.Contains("Entity")).Count() == 1)
                    {
                        _gameOver.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "You beat the Tutorial!";
                        gameOver = true;               
                    }
                    break;
                case "Level1Map":
                    if ( (int)WorldController.getPlayerTile().XPos == 0 && (int)WorldController.getPlayerTile().YPos == 25 )
                    {
                        _gameOver.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "You beat the Tutorial!";
                        gameOver = true;               
                    }
                    break;
            }

            if (gameOver)
            {
                _gameOver.SetActive(true);
            }
        }
    }
}