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
        SpawnCircle(5,5,3,new Color(255,0,0,0.5f));
    }

    public void SpawnTile(int xPos, int yPos, Color color, Tilemap tilemap, bool checkWalkable = true)
    {
        var walkingLayer = tileMap;

        DefaultTile node = null;
        foreach (var t in WorldController.Instance.BaseLayer)
        {
            if (t.XPos == xPos && t.YPos == yPos && (t.Walkable || !checkWalkable))
            {
                node = t;
                break;
            }
        }

        if (node != null)
        {
            var position = node.GameObject.transform.position;
            Vector3Int cell = walkingLayer.WorldToCell(new Vector3(position.x, 0, position.z));
            tilemap.SetTile(cell, tile);
            tilemap.SetColor(cell, color);
        }
    }

    public void SpawnCircle(int centerX, int centerY, int radius, Color color)
    {
        bool IsInsideCircle(int circleCenterX, int circleCenterY, int tileX, int tileY, int diameter) {
            float dx = circleCenterX - tileX;
            float dy = circleCenterY - tileY;
            float distanceSquared = dx*dx + dy*dy;
            return 4 * distanceSquared <= diameter*diameter;
        }

        // Calc bounds around circle
        int top = (int) Mathf.Ceil(centerY - radius);
        int bottom = (int) Mathf.Floor(centerY + radius);
        int left = (int) Mathf.Ceil(centerX - radius);
        int right  = (int) Mathf.Floor(centerX + radius);

        // Loop trough bounds, spawnTile at location in radius if tile is walkable
        for (int j = top; j <= bottom; j++) {
            for (int i = left; i <= right; i++) {
                if (IsInsideCircle(centerX, centerY, i, j, radius)) {
                    foreach (var t in WorldController.Instance.BaseLayer)
                    {
                        if (t.XPos == i && t.YPos == j && t.Walkable)
                        {
                            SpawnTile(i,j, color, rangeTileMap);
                        }
                    }
                    
                }
            }
        }
    }

    public void clearTileMap(Tilemap tilemap)
    {
        tilemap.ClearAllTiles();
    }
}
