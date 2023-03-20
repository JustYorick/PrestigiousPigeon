using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.Tilemaps;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private TileBase ruleTile;
    [SerializeField] private Tilemap walkingLayer;

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
            DrawPath(path);
            StartCoroutine(MoveSquares(path, gridLayout));
            playerPathNode.isWalkable = true;
            targetPathNode.isWalkable = false;
        }
    }

    // Finds the nearest PathNode based on world location coordinates; basically translates in world coordinates to the simplified ones.
    private PathNode FindNearestXYPathNode(Vector3 targetLocation, List<PathNode> pathNodesMap)
    {
        PathNode resultNode = pathNodesMap.OrderBy(item => Math.Abs(targetLocation.x - item.worldXPos)).ThenBy(item => Math.Abs(targetLocation.z - item.worldYPos)).ToList().FirstOrDefault();
        return resultNode;
    }

    IEnumerator MoveSquares(List<PathNode> path, GridLayout gridLayout)
    {
        foreach (PathNode pathNode in path)
        {
            transform.position = SnapCoordinateToGrid(new Vector3(pathNode.worldXPos, 0, pathNode.worldYPos), gridLayout); //fix!!!!
            yield return new WaitForSeconds(.2f);
            Vector3Int cell = walkingLayer.WorldToCell(new Vector3(pathNode.worldXPos, 0,pathNode.worldYPos));
            walkingLayer.SetTile(cell, null);
        }
    }

    private Vector3 SnapCoordinateToGrid(Vector3 position, GridLayout gridLayout)
    {
        Vector3Int cellPos = gridLayout.WorldToCell(position);
        Grid grid = gridLayout.gameObject.GetComponent<Grid>();
        position = grid.GetCellCenterWorld(cellPos);
        position.y = -0.5f; // Fix
        // Change Y position of player to match grid here
        return position;
    }

    void DrawPath(List<PathNode> pathNodes)
    {
        foreach (var node in pathNodes)
        {
            Vector3Int cell = walkingLayer.WorldToCell(new Vector3(node.worldXPos, 0,node.worldYPos));
            
            walkingLayer.SetTile(cell, ruleTile);
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
