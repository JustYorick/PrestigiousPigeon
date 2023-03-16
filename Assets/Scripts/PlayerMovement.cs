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

    /// <summary>
    /// Moves player from current location to new location
    /// </summary>
    /// <param name="targetLocation">Destination vector location in world</param>
    /// <param name="gridLayout">Layout of the grid the player should move within so the player can be centered</param>
    /// <param name="pathNodesMap">List of PathNodes that form a grid together, PathNodes have both simplified and in world coordinates</param>
    public void MovePlayer(Vector3 targetLocation, GridLayout gridLayout, List<PathNode> pathNodesMap)
    {
        //Currently only works for square grids not rectangular grids
        int width = (int) Math.Sqrt(pathNodesMap.Count); //temp
        int height = (int) Math.Sqrt(pathNodesMap.Count); //temp

        PlayerPathfinding playerPathfinding = new PlayerPathfinding(width, height, pathNodesMap);
        PathNode targetPathNode = FindNearestXYPathNode(targetLocation, pathNodesMap);
        
        PathNode playerPathNode = FindNearestXYPathNode(transform.position, pathNodesMap);
        List<PathNode> path = playerPathfinding.FindPath(playerPathNode.x, playerPathNode.y, targetPathNode.x, targetPathNode.y);

        if (path != null)
        {
            StartCoroutine(MoveSquares(path, gridLayout));
        }

        playerPathNode.isWalkable = true;
        targetPathNode.isWalkable = false;
    }

    //Finds the nearest PathNode based on world location coordinates; basically translates in world coordinates to the simplified ones.
    private PathNode FindNearestXYPathNode(Vector3 targetLocation, List<PathNode> pathNodesMap)
    {
        //PathNode resultNode = pathNodesMap.Aggregate((x, y) => Math.Abs(x.x - targetLocation.x) < Math.Abs(y.x - targetLocation.x) ? x : y);
        //resultNode = pathNodesMap.Aggregate((x, y) => Math.Abs(x.y - targetLocation.y) < Math.Abs(y.y - targetLocation.y) ? x : y);

        float closestX = pathNodesMap.OrderBy(item => Math.Abs(targetLocation.x - item.worldXPos)).Select(n => n.worldXPos).ToList().First();
        float closestY = pathNodesMap.OrderBy(item => Math.Abs(targetLocation.z - item.worldYPos)).Select(n => n.worldYPos).ToList().First();
        PathNode resultNode = pathNodesMap.Where(n => n.worldXPos == closestX && n.worldYPos == closestY).First();
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
