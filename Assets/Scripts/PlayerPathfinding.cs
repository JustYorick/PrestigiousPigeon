using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerPathfinding
{
    //???
    private int width;
    private int height;

    private List<PathNode> grid;
    private const int STANDARD_MOVE_COST = 10;
    private List<PathNode> openList;
    private List<PathNode> closedList;

    public PlayerPathfinding(int width, int height, List<PathNode> pathNodesMap)
    {
        this.grid = pathNodesMap;
        //grid = new List<PathNode>();
        //for (int x = 0; x < width; x++)
        //{
        //    for (int y = 0; y < height; y++)
        //    {
        //        grid.Add(new PathNode(x, y));
        //    }
        //}
        
        this.width = width;
        this.height = height;
    }

    public List<PathNode> FindPath(int startX, int startY, int endX, int endY)
    {
        PathNode startNode = grid.Where(n=>n.x == startX && n.y == startY).First();
        PathNode endNode = grid.Where(n => n.x == endX && n.y == endY).First();
        //PathNode startNode = GetNode(startX, startY);
        //PathNode endNode = GetNode(endX, endY);

        openList = new List<PathNode> { startNode };
        closedList = new List<PathNode>();

        //for (int x = 0; x < width; x++)
        //{
        //    for (int y = 0; y < height; y++)
        //    {
        //        //PathNode pathNode = new PathNode(x, y);
        //        PathNode pathNode = grid.Where(n => n.x == x && n.y == y).First();
        //        pathNode.gCost = 999999;
        //        pathNode.CalculateFCost();
        //        pathNode.previousNode = null;
        //        //grid.Add(pathNode);
        //    }
        //}

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
        //Debug.Log("randomgcost: "+grid[5].gCost);

        startNode.gCost = 0;
        startNode.hCost = CalculateDistanceCost(startNode, endNode);
        startNode.CalculateFCost();

        
        while(openList.Count > 0)
        {
            PathNode currentNode = GetLowestFCostNode(openList);
            if (currentNode.x==endNode.x && currentNode.y==endNode.y)
            {
                //Debug.Log("true");
                return CalculatePath(endNode);
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            //Debug.Log(GetNeighbourList(currentNode).Count);
            foreach (PathNode neighbourNode in GetNeighbourList(currentNode))
            {
                if (closedList.Contains(neighbourNode)) continue;

                if (!neighbourNode.isWalkable)
                {
                    closedList.Add(neighbourNode);
                    continue;
                }
                //Debug.Log("currentnode gcost:"+ currentNode.gCost);
                int tentativeGCost = currentNode.gCost + CalculateDistanceCost(currentNode, neighbourNode);
                //Debug.Log("tentative gcost:"+tentativeGCost);
                //Debug.Log("neigbournode gcost:" + neighbourNode.gCost);
                //Debug.Log("fcost:"+neighbourNode.fCost+", hcost:"+neighbourNode.hCost);
                if (tentativeGCost < neighbourNode.gCost)
                {
                    //Debug.Log("tentativecost");
                    neighbourNode.previousNode = currentNode;
                    neighbourNode.gCost = tentativeGCost;
                    neighbourNode.hCost = CalculateDistanceCost(neighbourNode, endNode);
                    neighbourNode.CalculateFCost();

                    if (!openList.Contains(neighbourNode))
                    {
                        openList.Add(neighbourNode);
                        //Debug.Log("Added");
                    }
                }
            }
        }

        //open list empty
        return null;
    }

    private List<PathNode> GetNeighbourList(PathNode currentNode)
    {
        List<PathNode> neighbourList = new List<PathNode>();

        // Left
        if (currentNode.x - 1 >= 0)
        {
            //Debug.Log("Left");
            neighbourList.Add(GetNode(currentNode.x - 1, currentNode.y));
        }

        // Right
        if (currentNode.x + 1 < width)
        {
            //Debug.Log("Right");
            neighbourList.Add(GetNode(currentNode.x + 1, currentNode.y));
        }

        // Down
        if (currentNode.y - 1 >= 0)
        {
            //Debug.Log("Down");
            neighbourList.Add(GetNode(currentNode.x, currentNode.y - 1));
        }

        // Up
        if (currentNode.y + 1 < height)
        {
            //Debug.Log("Up");
            neighbourList.Add(GetNode(currentNode.x, currentNode.y + 1));
        }

        return neighbourList;
    }

    public PathNode GetNode(int x, int y)
    {
        //TEMP!!
        //return grid.GetGridObject(x, y);
        //return new PathNode(x, y);
        PathNode pnode = grid.Where(n => n.x == x && n.y == y ).Select(n=>n).FirstOrDefault();
        //pnode.gCost = 99999;
        //Debug.Log("pnode gcost:" + pnode.gCost);
        //Debug.Log("gridsize" + grid.Count);
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
