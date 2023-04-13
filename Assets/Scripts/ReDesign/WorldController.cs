using System;
using System.Collections.Generic;
using System.Linq;
using ReDesign.Entities;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

namespace ReDesign
{
    public class WorldController : MonoBehaviour
    {
        public List<DefaultTile> BaseLayer;
        public static List<DefaultTile> ObstacleLayer;
        [SerializeField] private Tilemap baseTilemap;
        [SerializeField] private Tilemap obstacleTilemap;
        public GridLayout gridLayout;
        private List<GameObject> baseLayerChildren = new List<GameObject>();
        private List<GameObject> obstacleLayerChildren = new List<GameObject>();
        public static WorldController Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
                Destroy(gameObject);
            else
                Instance = this;
        }

        private void Start()
        {
            ReadWorld();
        }

        public void ReadWorld()
        {
            BaseLayer = new List<DefaultTile>();
            ObstacleLayer = new List<DefaultTile>();
            for (int i = 0; i < baseTilemap.transform.childCount; i++)
            {
                GameObject child = baseTilemap.transform.GetChild(i).gameObject;
                baseLayerChildren.Add(child);
            }

            for (int i = 0; i < obstacleTilemap.transform.childCount; i++)
            {
                GameObject child = obstacleTilemap.transform.GetChild(i).gameObject;
                if (child.activeInHierarchy)
                {
                    obstacleLayerChildren.Add(child);
                }
            }

            CreateGrid();
        }
        private void CreateGrid()
        {
            baseLayerChildren = baseLayerChildren.OrderBy(n => n.transform.position.x).ThenBy(n => n.transform.position.z).ToList();

            int counter = 0;
            for (int y = 0; y < Math.Sqrt(baseLayerChildren.Count); y++)
            {
                for (int x = 0; x < Math.Sqrt(baseLayerChildren.Count); x++)
                {
                    DefaultTile tile = new DefaultTile {
                        GameObject = baseLayerChildren[counter],
                        XPos = x,
                        YPos = y,
                        Walkable = true
                    };

                    if (baseLayerChildren[counter].CompareTag("Unwalkable"))
                    {
                        tile.Walkable = false;
                    }
                    BaseLayer.Add(tile);
                    counter++;
                }
            }

            // Adds unwalkable tiles based on tiles in the obstacleLayer
            foreach (GameObject child in obstacleLayerChildren)
            {
                DefaultTile resultNode = BaseLayer.OrderBy(item => Math.Abs(child.transform.position.x - item.GameObject.transform.position.x))
                    .ThenBy(item => Math.Abs(child.transform.position.z - item.GameObject.transform.position.z)).ToList().FirstOrDefault();
                resultNode.Walkable = false;

                DefaultTile enemy = new DefaultTile()
                {
                    XPos = resultNode.XPos,
                    YPos = resultNode.YPos,
                    GameObject = child,
                };

                ObstacleLayer.Add(enemy);
            }
        }

        public static List<Entity> getEntities()
        {
            List<Entity> outList = new List<Entity>();
            foreach (var tile in ObstacleLayer)
            {
                if (tile.GameObject.CompareTag("Entity"))
                {
                    outList.Add(tile.GameObject.GetComponent<Entity>());
                }
            }

            return outList;
        }

        public static DefaultTile getPlayerTile()
        {
            foreach (var tile in ObstacleLayer)
            {
                if (tile.GameObject.name.Equals("Player"))
                {
                    return tile;
                }
            }

            return null;
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