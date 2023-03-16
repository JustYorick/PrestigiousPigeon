using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class PlayerMovement : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }

    public void MovePlayer(Vector3 targetLocation, GridLayout gridLayout, List<PathNode> pathNodesMap)
    {
        int width = 10; //temp
        int height = 10; //temp
        PlayerPathfinding playerPathfinding = new PlayerPathfinding(width, height, pathNodesMap);
        PathNode targetPathNode = FindNearestXYPathNode(targetLocation, pathNodesMap);
        PathNode playerPathNode = FindNearestXYPathNode(transform.position, pathNodesMap);
        List<PathNode> path = playerPathfinding.FindPath(playerPathNode.x, playerPathNode.y, targetPathNode.x, targetPathNode.y);

        if (path != null)
        {
            Debug.Log("PATH LENGTH:" + path.Count);
            StartCoroutine(MoveSquares(path, gridLayout));
        }
    }

    private PathNode FindNearestXYPathNode(Vector3 targetLocation, List<PathNode> pathNodesMap)
    {
        //PathNode resultNode = pathNodesMap.Aggregate((x, y) => Math.Abs(x.x - targetLocation.x) < Math.Abs(y.x - targetLocation.x) ? x : y);
        //resultNode = pathNodesMap.Aggregate((x, y) => Math.Abs(x.y - targetLocation.y) < Math.Abs(y.y - targetLocation.y) ? x : y);

        float closestX = pathNodesMap.OrderBy(item => Math.Abs(targetLocation.x - item.worldXPos)).Select(n => n.worldXPos).ToList().First();
        Debug.Log("DIT IS CLOSESTX: " + closestX);
        float closestY = pathNodesMap.OrderBy(item => Math.Abs(targetLocation.z - item.worldYPos)).Select(n => n.worldYPos).ToList().First();
        Debug.Log("DIT IS CLOSESTY: " + closestY);
        PathNode resultNode = pathNodesMap.Where(n => n.worldXPos == closestX && n.worldYPos == closestY).First();
        Debug.Log("RESULTNODE: "+resultNode.x + resultNode.y + resultNode.worldXPos + resultNode.worldYPos);
        return resultNode;
    }

    IEnumerator MoveSquares(List<PathNode> path, GridLayout gridLayout)
    {
        foreach (PathNode pathNode in path)
        {
            transform.position = SnapCoordinateToGrid(new Vector3(pathNode.worldXPos, 0, pathNode.worldYPos), gridLayout); //fix!!!!
            yield return new WaitForSeconds(.2f);
        }
    }

    private Vector3 SnapCoordinateToGrid(Vector3 position, GridLayout gridLayout)
    {
        Vector3Int cellPos = gridLayout.WorldToCell(position);
        Grid grid = gridLayout.gameObject.GetComponent<Grid>();
        position = grid.GetCellCenterWorld(cellPos);
        position.y = -0.5f; //Fix
        //position.z = grid.GetCellCenterWorld(cellPos).z;
        return position;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
