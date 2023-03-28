using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerPathfinding
{
    private int width;
    private int height;

    private List<DefaultTile> grid;
    private const int STANDARD_MOVE_COST = 10;
    private List<DefaultTile> openList;
    private List<DefaultTile> closedList;

    public PlayerPathfinding(int width, int height, List<DefaultTile> pathNodesMap)
    {
        grid = pathNodesMap;
        this.width = width;
        this.height = height;
    }

    /// <summary>
    /// Pathfinding based on A* algorithm to find lowest cost path between two locations
    /// </summary>
    /// <param name="startX">Starting location X coordinate</param>
    /// <param name="startY">Starting location Y coordinate</param>
    /// <param name="endX">Destination location X coordinate</param>
    /// <param name="endY">Destination location Y coordinate</param>
    /// <returns>Ordered list of PathNodes to be traversed to go to destination</returns>
    public List<DefaultTile> FindPath(int startX, int startY, int endX, int endY)
    {
        DefaultTile startNode = grid.Where(n => n.XPos == startX && n.YPos == startY).First();
        DefaultTile endNode = grid.Where(n => n.XPos == endX && n.YPos == endY).First();

        openList = new List<DefaultTile> { startNode };
        closedList = new List<DefaultTile>();

        foreach (DefaultTile pathnode in grid)
        {
            pathnode.PathNode.gCost = 999999;
            pathnode.PathNode.CalculateFCost();
            pathnode.PathNode.previousNode = null;
        }

        startNode.PathNode.gCost = 0;
        startNode.PathNode.hCost = CalculateDistanceCost(startNode, endNode);
        startNode.PathNode.CalculateFCost();

        while (openList.Count > 0)
        {
            Debug.Log("openlist count");
            DefaultTile currentNode = GetLowestFCostNode(openList);
            if (currentNode.XPos == endNode.XPos && currentNode.YPos == endNode.YPos)
            {
                return CalculatePath(endNode);
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            foreach (DefaultTile neighbourNode in GetNeighbourList(currentNode))
            {
                if (closedList.Contains(neighbourNode)) continue;

                if (!neighbourNode.Walkable)
                {
                    closedList.Add(neighbourNode);
                    continue;
                }

                int tentativeGCost = currentNode.PathNode.gCost + CalculateDistanceCost(currentNode, neighbourNode);

                if (tentativeGCost < neighbourNode.PathNode.gCost)
                {
                    neighbourNode.PathNode.previousNode = currentNode;
                    neighbourNode.PathNode.gCost = tentativeGCost;
                    neighbourNode.PathNode.hCost = CalculateDistanceCost(neighbourNode, endNode);
                    neighbourNode.PathNode.CalculateFCost();

                    if (!openList.Contains(neighbourNode))
                    {
                        openList.Add(neighbourNode);
                    }
                }
            }
        }

        //when open list empty
        return null;
    }

    private List<DefaultTile> GetNeighbourList(DefaultTile currentNode)
    {
        List<DefaultTile> neighbourList = new List<DefaultTile>();

        // Left
        if (currentNode.XPos - 1 >= 0)
        {
            neighbourList.Add(GetNode(currentNode.XPos - 1, currentNode.YPos));
        }

        // Right
        if (currentNode.XPos + 1 < width)
        {
            neighbourList.Add(GetNode(currentNode.XPos + 1, currentNode.YPos));
        }

        // Down
        if (currentNode.YPos - 1 >= 0)
        {
            neighbourList.Add(GetNode(currentNode.XPos, currentNode.YPos - 1));
        }

        // Up
        if (currentNode.YPos + 1 < height)
        {
            neighbourList.Add(GetNode(currentNode.XPos, currentNode.YPos + 1));
        }

        Debug.Log("neightbour count"+neighbourList.Count);
        return neighbourList;
    }

    private DefaultTile GetNode(int x, int y)
    {
        DefaultTile pnode = grid.Where(n => n.XPos == x && n.YPos == y).Select(n => n).FirstOrDefault();
        Debug.Log("getnode"+pnode.XPos +","+pnode.YPos);
        return pnode;
    }

    private List<DefaultTile> CalculatePath(DefaultTile endNode)
    {
        List<DefaultTile> path = new List<DefaultTile>();
        path.Add(endNode);
        DefaultTile currentNode = endNode;
        while (currentNode.PathNode.previousNode != null)
        {
            Debug.Log("calculating path");
            path.Add(currentNode.PathNode.previousNode);
            currentNode = currentNode.PathNode.previousNode;
        }

        path.Reverse();
        return path;
    }

    private int CalculateDistanceCost(DefaultTile a, DefaultTile b)
    {
        int xDistance = Mathf.Abs(a.XPos - b.XPos);
        int yDistance = Mathf.Abs(a.YPos - b.YPos);
        return (int)(STANDARD_MOVE_COST * (xDistance + yDistance));
    }

    private DefaultTile GetLowestFCostNode(List<DefaultTile> pathNodeList)
    {
        DefaultTile lowestFCostNode = pathNodeList[0];
        for (int i = 1; i < pathNodeList.Count; i++)
        {
            if (pathNodeList[i].PathNode.fCost < lowestFCostNode.PathNode.fCost)
            {
                lowestFCostNode = pathNodeList[i];
            }
        }
        Debug.Log("lowest f"+lowestFCostNode.XPos);
        return lowestFCostNode;
    }
}