using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;
using UnityEngine.Tilemaps;
using System.Linq;

public class PlayerMovement : MonoBehaviour
{

    //public GameObject player;

    private void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {

    }

    //private static Vector3 GetMouseWorldPos()
    //{
    //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //    if (Physics.Raycast(ray, out RaycastHit raycastHit))
    //    {
    //        return raycastHit.point;
    //    }
    //    else
    //    {
    //        return Vector3.zero;
    //    }
    //}



    public void MovePlayer(Vector3 targetLocation, GridLayout gridLayout, Tilemap tilemap, List<PathNode> pathNodesMap)
    {
        int width = 10; //temp
        int height = 10; //temp
        PlayerPathfinding playerPathfinding = new PlayerPathfinding(width, height, pathNodesMap);
        //List<PathNode> path = playerPathfinding.FindPath((int)(this.transform.position.x), (int)(this.transform.position.z), (int)Math.Floor(targetLocation.x), (int)Math.Floor(targetLocation.z));
        //List<PathNode> path = playerPathfinding.FindPath(1, 1, 8, 8);
        //List<PathNode> path = playerPathfinding.FindPath((int)Math.Floor(transform.position.x), (int)Math.Floor(transform.position.z), (int)Math.Floor(targetLocation.x), (int)Math.Floor(targetLocation.z));

        //int targetX = 2;
        //int targetY = 5;
        PathNode targetPathNode = FindNearestXYPathNode(targetLocation, pathNodesMap);
        //Debug.Log("targetxy: " + targetX + ", " + targetY);

        PathNode playerPathNode = FindNearestXYPathNode(transform.position, pathNodesMap);
        //Debug.Log("playerxy: "+ playerX + ", " + playerY);
        //int playerX = 8;
        //int playerY = 2;
        List<PathNode> path = playerPathfinding.FindPath(playerPathNode.x, playerPathNode.y, targetPathNode.x, targetPathNode.y);
        //List<PathNode> path = playerPathfinding.FindPath(0, 1, 5, 9);
        if (path != null)
        {
            Debug.Log("PATH LENGTH:" + path.Count);
            StartCoroutine(MoveSquares(path, gridLayout, tilemap, pathNodesMap));
        }
    }

    private PathNode FindNearestXYPathNode(Vector3 targetLocation, List<PathNode> pathNodesMap)
    {
        ///targetLocation.x;
        //pathNodesMap
        //targetLocation.z;
        //float closestX = pathNodesMap.OrderBy(item => Math.Abs(targetLocation.x - item.worldXPos)).Select(n => n.worldXPos).ToList().First();

        //PathNode resultNode = pathNodesMap.Aggregate((x, y) => Math.Abs(x.x - targetLocation.x) < Math.Abs(y.x - targetLocation.x) ? x : y);
        //closestX = resultNode.x;

        //float list = pathNodesMap.OrderBy(item => Math.Abs(targetLocation.x - item.worldXPos)).Select(n => n.worldXPos).ToList().First();
        //Debug.Log("closest x: "+list);

        //float closestY = pathNodesMap.OrderBy(item => Math.Abs(targetLocation.y - item.worldYPos)).Select(n => n.worldYPos).ToList().First();
        //resultNode = pathNodesMap.Aggregate((x, y) => Math.Abs(x.y - targetLocation.y) < Math.Abs(y.y - targetLocation.y) ? x : y);
        //closestY = resultNode.y;

        //PathNode resultNode = pathNodesMap.Where(n => n.x == closestX && n.y == closestY).First();

        float closestX = pathNodesMap.OrderBy(item => Math.Abs(targetLocation.x - item.worldXPos)).Select(n => n.worldXPos).ToList().First();
        Debug.Log("DIT IS CLOSESTX: " + closestX);
        float closestY = pathNodesMap.OrderBy(item => Math.Abs(targetLocation.z - item.worldYPos)).Select(n => n.worldYPos).ToList().First();
        Debug.Log("DIT IS CLOSESTY: " + closestY);
        //PathNode resultNode = pathNodesMap.Where(n => (n.worldXPos - closestX < 0.4f || n.worldXPos - closestX < -0.4f) && (n.worldYPos - closestY < 0.4f || n.worldYPos - closestY < -0.4f)).First();
        //PathNode resultNode = pathNodesMap[34];
        PathNode resultNode = pathNodesMap.Where(n => n.worldXPos == closestX && n.worldYPos == closestY).First();
        Debug.Log("RESULTNODE: "+resultNode.x + resultNode.y + resultNode.worldXPos + resultNode.worldYPos);
        return resultNode;
    }

    //private void OnMouseDown()
    //{
    //    int width = 14; //temp
    //    int height = 14; //temp
    //    Vector3 pos = GetMouseWorldPos();

    //    PlayerPathfinding playerPathfinding = new PlayerPathfinding(width, height);
    //    //Debug.Log(""+(int)Math.Floor(player.transform.position.x) + (int)Math.Floor(player.transform.position.z) + (int)Math.Floor(pos.x) + (int)Math.Floor(pos.z));
    //    List<PathNode> path = playerPathfinding.FindPath((int)Math.Floor(player.transform.position.x), (int)Math.Floor(player.transform.position.z), (int)Math.Floor(pos.x), (int)Math.Floor(pos.z));
    //    if (path != null)
    //    {
    //        Debug.Log("PATH LENGTH:" + path.Count);
    //        StartCoroutine(MoveSquares(path));
    //    }
    //}

    IEnumerator MoveSquares(List<PathNode> path, GridLayout gridLayout, Tilemap tilemap, List<PathNode> pathNodesMap)
    {
        foreach (PathNode pathNode in path)
        {
            //Debug.Log(pathNode.x + "," + pathNode.y);

            Grid grid = gridLayout.gameObject.GetComponent<Grid>();
             



            this.transform.position = SnapCoordinateToGrid(new Vector3(pathNode.worldXPos, 0, pathNode.worldYPos), gridLayout); //fix!!!!
            //this.transform.position = SnapCoordinateToGrid(new Vector3(5f, 5f, 5f), gridLayout);
            //this.transform.position = new Vector3(pathNode.x, 0, pathNode.y);
            yield return new WaitForSeconds(.2f);
        }
    }

    private Vector3 SnapCoordinateToGrid(Vector3 position, GridLayout gridLayout)
    {
        //Vector3Int cellPos = gridLayout.WorldToCell(position);
        ///position = grid.GetCellCenterWorld(cellPos);
        ///
        Vector3Int cellPos = gridLayout.WorldToCell(position);
        Grid grid = gridLayout.gameObject.GetComponent<Grid>();
        position = grid.GetCellCenterWorld(cellPos);
        position.y = -0.5f;
        //position.z = grid.GetCellCenterWorld(cellPos).z;
        return position;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
