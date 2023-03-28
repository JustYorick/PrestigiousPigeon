using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ReDesign;

public class DefaultTile : Tile
{
    public bool Walkable { get; set; }
    public PathNode PathNode { get; set; }

    public DefaultTile()
    {
        this.PathNode = new PathNode();
        //Walkable = true;
    }
}
