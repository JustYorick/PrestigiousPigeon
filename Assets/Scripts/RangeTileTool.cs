using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ReDesign;
using ReDesign.Entities;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RangeTileTool : MonoBehaviour
{
    [SerializeField] private Tilemap tileMap;
    
    [SerializeField] public Tilemap rangeTileMap;
    
    [SerializeField] private RuleTile tile;

    [SerializeField] private ManaSystem manaSystem;
    
    public static RangeTileTool Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
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

    public void SpawnCircle(int centerX, int centerY, int minRange, int maxRange, Color color)
    {
        maxRange = maxRange*2;
        minRange = minRange * 2 - 1;
        
        bool IsInsideCircle(int circleCenterX, int circleCenterY, int tileX, int tileY, int diameter) {
            float dx = circleCenterX - tileX;
            float dy = circleCenterY - tileY;
            float distanceSquared = dx*dx + dy*dy;
            return 4 * distanceSquared <= diameter*diameter;
        }

        // Calc bounds around circle
        int top = (int) Mathf.Ceil(centerY - maxRange);
        int bottom = (int) Mathf.Floor(centerY + maxRange);
        int left = (int) Mathf.Ceil(centerX - maxRange);
        int right  = (int) Mathf.Floor(centerX + maxRange);

        // Loop trough bounds, spawnTile at location in radius if tile is walkable
        for (int j = top; j <= bottom; j++) {
            for (int i = left; i <= right; i++) {
                if (IsInsideCircle(centerX, centerY, i, j, maxRange) &&
                    !IsInsideCircle(centerX, centerY, i, j, minRange)) {
                    foreach (var t in WorldController.Instance.BaseLayer)
                    {
                        if (t.XPos == i && t.YPos == j)
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

    public void drawMoveRange(DefaultTile tile, int range)
    {
        clearTileMap(rangeTileMap);
        int widthAndHeight = (int)Mathf.Sqrt(WorldController.Instance.BaseLayer.Count);
        PlayerPathfinding pf = new PlayerPathfinding(widthAndHeight, widthAndHeight, WorldController.Instance.BaseLayer);
        int maxRange = range*2;
        List<DefaultTile> tilesToDraw = new List<DefaultTile>();

        bool IsInsideCircle(int circleCenterX, int circleCenterY, int tileX, int tileY, int diameter) {
            float dx = circleCenterX - tileX;
            float dy = circleCenterY - tileY;
            float distanceSquared = dx*dx + dy*dy;
            return 4 * distanceSquared <= diameter*diameter;
        }

        // Calc bounds around circle
        int top = (int) Mathf.Ceil(tile.YPos - maxRange);
        int bottom = (int) Mathf.Floor(tile.YPos + maxRange);
        int left = (int) Mathf.Ceil(tile.XPos - maxRange);
        int right  = (int) Mathf.Floor(tile.XPos + maxRange);

        // Loop trough bounds, spawnTile at location in radius if tile is walkable
        for (int j = top; j <= bottom; j++) {
            for (int i = left; i <= right; i++) {
                if (IsInsideCircle(tile.XPos, tile.YPos, i, j, maxRange))
                {
                    foreach (var t in WorldController.Instance.BaseLayer)
                    {
                        if (t.XPos == i && t.YPos == j && t.Walkable)
                        {
                            List<DefaultTile> tiles = pf.FindPath(tile.XPos, tile.YPos, t.XPos, t.YPos);
                            if (tiles != null)
                            {
                                tiles.RemoveAt(0);
                                if (tiles.Count >= range)
                                {
                                    
                                    tiles = tiles.GetRange(0, range);
                                }
                                tilesToDraw.AddRange(tiles); 
                            }
                        }
                    }
                }
            }
        }

        tilesToDraw = tilesToDraw.Distinct().ToList();
        foreach (var t in tilesToDraw)
        {
            SpawnTile(t.XPos, t.YPos, new Color(0,0,255,0.5f), rangeTileMap);
        }
    }
}
