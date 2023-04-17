using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ReDesign;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RangeTileTool : MonoBehaviour
{
    [SerializeField] private Tilemap tileMap;
    
    [SerializeField] private Tilemap rangeTileMap;
    
    [SerializeField] private RuleTile tile;
    
    public static RangeTileTool Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    private void Start()
    {
        //SpawnTile(0,0,new Color(255,0,0,0.5f));
    }

    public void SpawnTile(int xPos, int yPos, Color color)
    {
        var walkingLayer = tileMap;
        var rangeTileTileMap = rangeTileMap;
        
        DefaultTile node;
        node = null;
        foreach (var t in WorldController.Instance.BaseLayer)
        {
            if (t.XPos == xPos && t.YPos == yPos)
            {
                node = t;
                break;
            }
        }

        Vector3Int cell = walkingLayer.WorldToCell(new Vector3(node.GameObject.transform.position.x, 0, node.GameObject.transform.position.z));
        rangeTileTileMap.SetTile(cell, tile);
        rangeTileTileMap.SetColor(cell, color);
    }

    public void clearTileMap()
    {
        rangeTileMap.ClearAllTiles();
    }
}
