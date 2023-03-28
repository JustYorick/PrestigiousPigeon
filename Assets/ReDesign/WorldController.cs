using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ReDesign
{
    public class WorldController : MonoBehaviour
    {
        public List<Tile> BaseLayer;
        public List<Tile> ObstacleLayer;
        public static WorldController Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
                Destroy(gameObject);
            else
                Instance = this;
        }

        public void ReadWorld(Scene scene)
        {
            //load world from scene to tile lists
        }

        public List<Dictionary<string, List<int>>> getTiles(int XPos, int YPos)
        {
            List<Dictionary<string, List<int>>> output = null;
            foreach (var tile in BaseLayer)
            {
                if (tile.XPos == XPos && tile.YPos == YPos)
                {
                   output.Add( new Dictionary<string, List<int>>()
                   {
                       {
                           "BaseLayer", 
                           new List<int>(){XPos, YPos}
                       }
                   });
                }
            }
            
            foreach (var tile in ObstacleLayer)
            {
                if (tile.XPos == XPos && tile.YPos == YPos)
                {
                    output.Add( new Dictionary<string, List<int>>()
                    {
                        {
                            "ObstacleLayer", 
                            new List<int>(){XPos, YPos}
                        }
                    });
                }
            }

            return output;
        }
    }
}