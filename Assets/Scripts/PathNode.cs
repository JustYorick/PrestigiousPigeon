using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode
{
    // Simplified coordinates used for pathfinding
    //public int x;
    //public int y;

    // In world coordinates
    //public float worldXPos;
    //public float worldYPos;

    public int gCost;
    public int hCost;
    public int fCost;

    //public bool isWalkable;
    //public GameObject obstacle;

    public DefaultTile previousNode;
    public PathNode()
    {
        //this.x = x;
        //this.y = y;
    }

    public void CalculateFCost()
    {
        fCost = gCost + hCost;
    }

}
