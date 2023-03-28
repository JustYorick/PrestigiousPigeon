using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode
{
    public int gCost;
    public int hCost;
    public int fCost;

    public DefaultTile previousNode;
    public PathNode()
    {
    }

    public void CalculateFCost()
    {
        fCost = gCost + hCost;
    }

}
