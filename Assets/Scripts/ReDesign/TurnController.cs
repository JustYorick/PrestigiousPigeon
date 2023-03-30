using System.Collections.Generic;
using ReDesign.Entities;
using UnityEngine;

namespace ReDesign
{
    public class TurnController : MonoBehaviour
    {
        private static TurnController _instance;
        public static TurnController Instance { get { return _instance; } }
        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            } else {
                _instance = this;
            }
        }


        public static int turnCount = 0;
        private static int _turnPart = 0;
        
        private static List<Entity> _entities = new List<Entity>()
        {
            new Player(),
            new Slime()
        };

        public static void ResolveTurn()
        {
            if (_turnPart < _entities.Count-1)
            {
                _entities[_turnPart].NextAction();
                _turnPart++;
            }

            _turnPart = 0;
            turnCount++;
        }
    }
}