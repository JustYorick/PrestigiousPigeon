using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode
{
    //Simplified coordinates used in pathfinding
    public int x;
    public int y;

    //In world coordinates
    public float worldXPos;
    public float worldYPos;

    public int gCost;
    public int hCost;
    public int fCost;

    public bool isWalkable;

    public PathNode previousNode;
    public PathNode(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public void CalculateFCost()
    {
        fCost = gCost + hCost;
    }

}
