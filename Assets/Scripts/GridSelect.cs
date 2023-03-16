using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridSelect : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    ///*[SerializeField]*/ private GridLayout gridLayout;
    //[SerializeField] private Grid grid;
    [SerializeField] private GameObject player;
    [SerializeField] private GridLayout gridLayout;
    [SerializeField] private Tilemap tilemap;
    private Grid grid;
    public List<GameObject> children = new List<GameObject>();
    public List<PathNode> pathNodesMap = new List<PathNode>();

    // Start is called before the first frame update
    void Start()
    {
        grid = gridLayout.gameObject.GetComponent<Grid>();


        for (int i = 0; i < tilemap.transform.childCount; i++)
        {
            GameObject child = tilemap.transform.GetChild(i).gameObject;
            children.Add(child);
        }
        CreateGrid();

        //gridLayout = grid.GetComponent<GridLayout>();
        //grid = gridLayout.gameObject.GetComponent<Grid>(); //null ref
    }

    private void CreateGrid()
    {
        children = children.OrderBy(n=>n.transform.position.x).ThenBy(n=>n.transform.position.z).ToList();

        //var order = from GameObject in children orderby GameObject.transform.position.x, GameObject.transform.position.z select GameObject;
       // children = (List<GameObject>)order;
        int counter = 0;
        for (int y = 0; y < Math.Sqrt(children.Count); y++)
        {
            for (int x = 0; x < Math.Sqrt(children.Count); x++)
            {
                PathNode pn = new PathNode(x, y);
                pn.worldXPos = children[counter].transform.position.x;
                pn.worldYPos = children[counter].transform.position.z;
                pn.isWalkable = true;
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
        //tilemap.ClearAllTiles();
        //tilemap.
        //Debug.Log("Tilemap:"+ "");
        Vector3 pos = GetMouseWorldPos();
        //player.MovePlayer();
        Debug.Log("clicked x, y: "+pos.x+", "+pos.z);
        //TERUGDOEN player.GetComponent<PlayerMovement>().MovePlayer(pos);
        //Vector3 v3 = SnapCoordinateToGrid(pos);
        //Debug.Log("snapped pos:"+v3.x);
        player.GetComponent<PlayerMovement>().MovePlayer(pos, gridLayout, tilemap, pathNodesMap);

        //float closestX = pathNodesMap.OrderBy(item => Math.Abs(pos.x - item.worldXPos)).Select(n => n.worldXPos).ToList().First();
        //Debug.Log("CLOSEST X: " + closestX);
        //float closestY = pathNodesMap.OrderBy(item => Math.Abs(pos.z - item.worldYPos)).Select(n => n.worldYPos).ToList().First();
        //Debug.Log("CLOSEST Y: " + closestY);

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

        //Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        ////mouseWorldPosition.z = 0f;
        //Vector3Int coordinate = grid.WorldToCell(mouseWorldPosition);
        //Debug.Log("y" + coordinate.y);
        //return mouseWorldPosition;
    }

}
