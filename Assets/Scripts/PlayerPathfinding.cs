using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerPathfinding
{
    //temp
    private int width;
    private int height;

    private List<PathNode> grid;
    private const int STANDARD_MOVE_COST = 10;
    private List<PathNode> openList;
    private List<PathNode> closedList;

    public PlayerPathfinding(int width, int height, List<PathNode> pathNodesMap)
    {
        grid = pathNodesMap;
        this.width = width;
        this.height = height;
    }

    public List<PathNode> FindPath(int startX, int startY, int endX, int endY)
    {
        PathNode startNode = grid.Where(n=>n.x == startX && n.y == startY).First();
        PathNode endNode = grid.Where(n => n.x == endX && n.y == endY).First();

        openList = new List<PathNode> { startNode };
        closedList = new List<PathNode>();

        foreach (PathNode pathnode in grid)
        {
            pathnode.gCost = 999999;
            pathnode.CalculateFCost();
            pathnode.previousNode = null;
            pathnode.isWalkable = true; //temp!!!
            if (pathnode.x == 7 && pathnode.y != 5) //temp row unwalkable except 1 square
            {
                pathnode.isWalkable = false;
            }
        }

        startNode.gCost = 0;
        startNode.hCost = CalculateDistanceCost(startNode, endNode);
        startNode.CalculateFCost();
        
        while(openList.Count > 0)
        {
            PathNode currentNode = GetLowestFCostNode(openList);
            if (currentNode.x==endNode.x && currentNode.y==endNode.y)
            {
                return CalculatePath(endNode);
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            foreach (PathNode neighbourNode in GetNeighbourList(currentNode))
            {
                if (closedList.Contains(neighbourNode)) continue;

                if (!neighbourNode.isWalkable)
                {
                    closedList.Add(neighbourNode);
                    continue;
                }

                int tentativeGCost = currentNode.gCost + CalculateDistanceCost(currentNode, neighbourNode);

                if (tentativeGCost < neighbourNode.gCost)
                {
                    neighbourNode.previousNode = currentNode;
                    neighbourNode.gCost = tentativeGCost;
                    neighbourNode.hCost = CalculateDistanceCost(neighbourNode, endNode);
                    neighbourNode.CalculateFCost();

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

    private List<PathNode> GetNeighbourList(PathNode currentNode)
    {
        List<PathNode> neighbourList = new List<PathNode>();

        // Left
        if (currentNode.x - 1 >= 0)
        {
            neighbourList.Add(GetNode(currentNode.x - 1, currentNode.y));
        }

        // Right
        if (currentNode.x + 1 < width)
        {
            neighbourList.Add(GetNode(currentNode.x + 1, currentNode.y));
        }

        // Down
        if (currentNode.y - 1 >= 0)
        {
            neighbourList.Add(GetNode(currentNode.x, currentNode.y - 1));
        }

        // Up
        if (currentNode.y + 1 < height)
        {
            neighbourList.Add(GetNode(currentNode.x, currentNode.y + 1));
        }

        return neighbourList;
    }

    public PathNode GetNode(int x, int y)
    {
        PathNode pnode = grid.Where(n => n.x == x && n.y == y ).Select(n=>n).FirstOrDefault();
        return pnode;
    }

    private List<PathNode> CalculatePath(PathNode endNode)
    {
        List<PathNode> path = new List<PathNode>();
        path.Add(endNode);
        PathNode currentNode = endNode;
        while (currentNode.previousNode != null)
        {
            path.Add(currentNode.previousNode);
            currentNode = currentNode.previousNode;
        }

        path.Reverse();
        return path;
    }

    private int CalculateDistanceCost(PathNode a, PathNode b)
    {
        int xDistance = Mathf.Abs(a.x - b.x);
        int yDistance = Mathf.Abs(a.y - b.y);
        //int remaining = Mathf.Abs(xDistance - yDistance);
        return (int)(STANDARD_MOVE_COST * (xDistance+yDistance));
    }

    private PathNode GetLowestFCostNode(List<PathNode> pathNodeList)
    {
        PathNode lowestFCostNode = pathNodeList[0];
        for (int i = 1; i < pathNodeList.Count; i++)
        {
            if (pathNodeList[i].fCost < lowestFCostNode.fCost)
            {
                lowestFCostNode = pathNodeList[i];
            }
        }

        return lowestFCostNode;
    }
}
