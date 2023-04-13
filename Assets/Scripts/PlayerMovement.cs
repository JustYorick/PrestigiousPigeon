using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.Tilemaps;
using ReDesign;
using ReDesign.Entities;

[RequireComponent(typeof(ManaSystem))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private TileBase ruleTile;
    [SerializeField] private Tilemap walkingLayer;
    private ManaSystem manaSystem;
    [SerializeField] private bool predrawPath = true;
    private List<DefaultTile> predrawnPath = new List<DefaultTile>();

    // Start is called before the first frame update
    void Start()
    {
        manaSystem = GetComponent<ManaSystem>();
    }

    /// <summary>
    /// Moves player from current location to new location
    /// </summary>
    /// <param name="targetLocation">Destination vector location in world</param>
    /// <param name="gridLayout">Layout of the grid the player should move within so the player can be centered</param>
    /// <param name="pathNodesMap">List of PathNodes that form a grid together, PathNodes have both simplified and in world coordinates</param>
    public void MovePlayer(Vector3 targetLocation, GridLayout gridLayout, List<DefaultTile> pathNodesMap)
    {
        //Currently only works for square grids not rectangular grids
        int width = (int) Math.Sqrt(pathNodesMap.Count); //temp
        int height = (int) Math.Sqrt(pathNodesMap.Count); //temp

        PlayerPathfinding playerPathfinding = new PlayerPathfinding(width, height, pathNodesMap);

        DefaultTile targetPathNode = FindNearestXYPathNode(targetLocation, pathNodesMap);
        DefaultTile playerPathNode = FindNearestXYPathNode(transform.position, pathNodesMap);

        List<DefaultTile> path = playerPathfinding.FindPath(playerPathNode.XPos, playerPathNode.YPos, targetPathNode.XPos, targetPathNode.YPos);
        int pathCost = path == null? 0 : path.Count - 1;

        if (path != null && pathCost <= manaSystem.GetMana())
        {
            predrawPath = false;
            DrawPath(path);
            StartCoroutine(MoveSquares(path, gridLayout));
            playerPathNode.Walkable = true;
            targetPathNode.Walkable = false;
            manaSystem.UseMana(pathCost);

            //List<DefaultTile> list = new BasicIceSpell().GetTargetLocations(5, 5);
            //foreach (DefaultTile dt in list)
            //{
            //    Debug.Log("x: " + dt.XPos + "y: " + dt.YPos);
            //}
        }
    }

    // Finds the nearest PathNode based on world location coordinates; basically translates in world coordinates to the simplified ones.
    public DefaultTile FindNearestXYPathNode(Vector3 targetLocation, List<DefaultTile> pathNodesMap)
    {
        DefaultTile resultNode = pathNodesMap.OrderBy(item => Math.Abs(targetLocation.x - item.GameObject.transform.position.x)).ThenBy(item => Math.Abs(targetLocation.z - item.GameObject.transform.position.z)).ToList().FirstOrDefault();
        return resultNode;
    }

    IEnumerator MoveSquares(List<DefaultTile> path, GridLayout gridLayout)
    {
        foreach (DefaultTile pathNode in path)
        {
            transform.position = SnapCoordinateToGrid(new Vector3(pathNode.GameObject.transform.position.x, transform.position.y, pathNode.GameObject.transform.position.z), gridLayout); //fix!!!!
            yield return new WaitForSeconds(.2f);
            Vector3Int cell = walkingLayer.WorldToCell(new Vector3(pathNode.GameObject.transform.position.x, transform.position.y, pathNode.GameObject.transform.position.z));
            walkingLayer.SetTile(cell, null);
        }

        DefaultTile dt = WorldController.ObstacleLayer.Where(o => o.XPos == path.First().XPos && o.YPos == path.First().YPos).FirstOrDefault();
        dt.XPos = path.Last().XPos;
        dt.YPos = path.Last().YPos;


        GetComponent<Player>().finishedMoving = true;
        manaSystem.StartTurn();
        predrawPath = true;
    }

    private Vector3 SnapCoordinateToGrid(Vector3 position, GridLayout gridLayout)
    {
        Vector3Int cellPos = gridLayout.WorldToCell(position);
        Grid grid = gridLayout.gameObject.GetComponent<Grid>();
        position = new Vector3(grid.GetCellCenterWorld(cellPos).x, position.y, grid.GetCellCenterWorld(cellPos).z);
        // Change Y position of player to match grid here
        return position;
    }

    void DrawPath(List<DefaultTile> pathNodes)
    {
        foreach (var node in pathNodes)
        {
            Vector3Int cell = walkingLayer.WorldToCell(new Vector3(node.GameObject.transform.position.x, 0, node.GameObject.transform.position.z));
            
            walkingLayer.SetTile(cell, ruleTile);
        }
    }

    public void ShowPath(Vector3 targetLocation, GridLayout gridLayout, List<DefaultTile> pathNodesMap){
        if(predrawPath && predrawnPath != null){
            // Erase the old path
            foreach (DefaultTile pathNode in predrawnPath){
                Vector3Int cell = walkingLayer.WorldToCell(new Vector3(pathNode.GameObject.transform.position.x, 0, pathNode.GameObject.transform.position.z));
                walkingLayer.SetTile(cell, null);
            }
        }
        if(predrawPath){
            //Currently only works for square grids not rectangular grids
            int width = (int) Math.Sqrt(pathNodesMap.Count); //temp
            int height = (int) Math.Sqrt(pathNodesMap.Count); //temp

            PlayerPathfinding playerPathfinding = new PlayerPathfinding(width, height, pathNodesMap);

            DefaultTile targetPathNode = FindNearestXYPathNode(targetLocation, pathNodesMap);
            DefaultTile playerPathNode = FindNearestXYPathNode(transform.position, pathNodesMap);

            List<DefaultTile> path = playerPathfinding.FindPath(playerPathNode.XPos, playerPathNode.YPos, targetPathNode.XPos, targetPathNode.YPos);

            if (path != null && path.Count - 1 <= manaSystem.GetMana()){
                DrawPath(path);
            }
            predrawnPath = path;
        }
    }
}
