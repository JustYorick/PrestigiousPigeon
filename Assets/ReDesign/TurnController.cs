using System.Collections.Generic;
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


        public int turnCount = 0;
        private int turnPart = 0;
        
        private List<Entity> entities = new List<Entity>()
        {
            new Player()
        };

        public void ResolveTurn()
        {
            if (turnPart < entities.Count-1)
            {
                entities[turnPart].NextAction();
                turnPart++;
            }

            turnPart = 0;
            turnCount++;
        }
    }
}