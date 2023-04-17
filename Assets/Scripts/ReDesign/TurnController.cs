using System.Collections.Generic;
using ReDesign.Entities;
using UnityEngine;

namespace ReDesign
{
    public class TurnController : MonoBehaviour
    {
        private static TurnController _instance;
        public static TurnController Instance { get { return _instance; } }
        public static int TurnCount = 0;
        private static int _turnPart = 0;
        private static bool controlsHidden;
        private static GameObject _controlsPanel;
        private static List<Entity> _entities = new List<Entity>();

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

            controlsHidden = true;
            _controlsPanel = GameObject.Find("Controls");
            _controlsPanel.SetActive(false);
        }

        private void Start()
        {
            FillEntityList();
            
            //starts the first turn loop
            ResolveNextTurn();
        }

        public static void ResolveNextTurn()
        {
            FillEntityList();
            if (_turnPart < _entities.Count)
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
            //FillEntityList();
        }

        public static void FillEntityList()
        {
            _entities = WorldController.getEntities();
            //Debug.Log("entities count: "+_entities.Count);
            //Debug.Log("obst count : " + WorldController.ObstacleLayer.Count);
        }

        public void ShowControls ()
        {
            if (controlsHidden)
            {
                _controlsPanel.SetActive(true);
                controlsHidden = false;
            }
            else
            {
                _controlsPanel.SetActive(false);
                controlsHidden = true;
            }
        }
    }
}