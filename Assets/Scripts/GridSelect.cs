using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridSelect : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GridLayout gridLayout;
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private Tilemap obstacleLayer;

    private List<GameObject> children = new List<GameObject>();
    private List<GameObject> obstacleLayerChildren = new List<GameObject>();
    private List<PathNode> pathNodesMap = new List<PathNode>();

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < tilemap.transform.childCount; i++)
        {
            GameObject child = tilemap.transform.GetChild(i).gameObject;
            children.Add(child);
        }

        for (int i = 0; i < obstacleLayer.transform.childCount; i++)
        {
            GameObject child = obstacleLayer.transform.GetChild(i).gameObject;
            if (child.activeInHierarchy)
            {
                obstacleLayerChildren.Add(child);
            }
        }

        CreateGrid();
    }

    //Creates square grid of PathNodes based on objects in children, origin of the grid is 0,0
    //Currently only works for perfectly square grids
    private void CreateGrid()
    {
        children = children.OrderBy(n=>n.transform.position.x).ThenBy(n=>n.transform.position.z).ToList();

        int counter = 0;
        for (int y = 0; y < Math.Sqrt(children.Count); y++)
        {
            for (int x = 0; x < Math.Sqrt(children.Count); x++)
            {
                PathNode pn = new PathNode(x, y)
                {
                    worldXPos = children[counter].transform.position.x,
                    worldYPos = children[counter].transform.position.z,
                    isWalkable = true
                };

                if (children[counter].CompareTag("Unwalkable"))
                {
                    pn.isWalkable = false;
                }

                pathNodesMap.Add(pn);
                counter++;
            }
        }

        //Adds unwalkable tiles based on tiles in the obstacleLayer
        foreach(GameObject child in obstacleLayerChildren)
        {
            float closestX = pathNodesMap.OrderBy(item => Math.Abs(child.transform.position.x - item.worldXPos)).Select(n => n.worldXPos).ToList().First();
            float closestY = pathNodesMap.OrderBy(item => Math.Abs(child.transform.position.z - item.worldYPos)).Select(n => n.worldYPos).ToList().First();
            PathNode resultNode = pathNodesMap.Where(n => n.worldXPos == closestX && n.worldYPos == closestY).First();
            resultNode.isWalkable = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnMouseDown()
    {
        Vector3 pos = GetMouseWorldPos();
        player.GetComponent<PlayerMovement>().MovePlayer(pos, gridLayout, pathNodesMap);
    }

    private Vector3 GetMouseWorldPos()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            return raycastHit.point;
        }
        else
        {
            return Vector3.zero;
        }
    }

}
