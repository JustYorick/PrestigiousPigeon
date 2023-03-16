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

    private List<GameObject> children = new List<GameObject>();
    private List<PathNode> pathNodesMap = new List<PathNode>();

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < tilemap.transform.childCount; i++)
        {
            GameObject child = tilemap.transform.GetChild(i).gameObject;
            children.Add(child);
        }

        CreateGrid();
    }

    //Creates square grid of PathNodes based on objects in children
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
                pathNodesMap.Add(pn);
                counter++;
            }
        }

        //foreach (GameObject g in children)
        //{
        //    Debug.Log(g.transform.position.x + ", " + g.transform.position.y + ", " + g.transform.position.z);
        //}

        //foreach (PathNode pn in pathNodesMap)
        //{
        //    Debug.Log(pn.x + ", " + pn.y +", "+pn.worldXPos+", "+pn.worldYPos);
        //}
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
