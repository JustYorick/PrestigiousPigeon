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
        private static int _turnPart = -1;
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
        }

        private void Start()
        {
            FillEntityList();
            
            //starts the first turn loop
            ResolveNextTurn();
        }

        public static void ResolveNextTurn()
        {
            if (_turnPart < _entities.Count-1)
            {
                _turnPart++;
                _entities[_turnPart].NextAction();
            }

            //_turnPart = 0;
            TurnCount++;
            FillEntityList();
        }

        public static void FillEntityList()
        {
            _entities = WorldController.getEntities();
        }
    }
}