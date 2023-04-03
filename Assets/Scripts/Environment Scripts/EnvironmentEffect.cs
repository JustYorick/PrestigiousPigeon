using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using ReDesign;

public class EnvironmentEffect : MonoBehaviour
{
    // Layers
    [SerializeField] private GameObject baseLayer;
    [SerializeField] private GameObject obstacleLayer;

    // Tiles
    [SerializeField] private GameObject iceTile;
    [SerializeField] private GameObject waterTile;
    [SerializeField] private GameObject treeTile;
    [SerializeField] private GameObject bridgeTile;

    /// <summary>
    /// Makes all relevant tiles react to fire environment effect
    /// </summary>
    /// <param name="pathNodes">List of nodes from the pathNodesMap that are in the range of the spell/effect used</param>
    public void FireEnvironmentEffects(List<DefaultTile> pathNodes)
    {
        ChangeIceTilesToWater(pathNodes);
        ChangeBridgeTileToWater(pathNodes);
        ChangeTreeTileToNothing(pathNodes);
    }

    /// <summary>
    /// Makes all relevant tiles react to ice environment effect
    /// </summary>
    /// <param name="pathNodes">List of nodes from the pathNodesMap that are in the range of the spell/effect used</param>
    public void IceEnvironmentEffects(List<DefaultTile> pathNodes)
    {
        ChangeWaterTilesToIce(pathNodes);
    }

    // Ice
    private void ChangeWaterTilesToIce(List<DefaultTile> pathNodes)
    {
        List<GameObject> tiles = GetChildObjects(baseLayer.GetComponent<Tilemap>());
        foreach (DefaultTile pn in pathNodes)
        {
            GameObject obj = FindNearestGameObjectTile(pn, tiles);
            if (obj.name.ToLower().Contains("water"))
            {
                DefaultTile targetTile = WorldController.Instance.BaseLayer.Where(t => t.XPos == pn.XPos && t.YPos == pn.YPos).FirstOrDefault();
                targetTile.Walkable = true;

                GameObject newTile = GameObject.Instantiate(iceTile);
                newTile.transform.position = obj.transform.position;
                Destroy(obj);
                newTile.transform.SetParent(baseLayer.transform);
                targetTile.GameObject = newTile;
            }
        }
    }

    // Fire
    private void ChangeIceTilesToWater(List<DefaultTile> pathNodes)
    {
        List<GameObject> obstacleTiles = GetChildObjects(obstacleLayer.GetComponent<Tilemap>());
        List<GameObject> tiles = GetChildObjects(baseLayer.GetComponent<Tilemap>());
        List<GameObject> entities = GetChildObjects(obstacleLayer.GetComponent<Tilemap>());
        foreach (DefaultTile pn in pathNodes)
        {
            GameObject obj = FindNearestGameObjectTile(pn, tiles);
            
            if (obj.name.ToLower().Contains("ice"))
            {
                DefaultTile targetTile = WorldController.Instance.BaseLayer.Where(t => t.XPos == pn.XPos && t.YPos == pn.YPos).FirstOrDefault();
                
                Debug.Log("IF gehaald");
                WorldController.Instance.BaseLayer.Where(t => t.XPos == pn.XPos && t.YPos == pn.YPos).FirstOrDefault().Walkable = false;
                GameObject newTile = GameObject.Instantiate(waterTile);
                newTile.transform.position = obj.transform.position;
                Destroy(obj);
                newTile.transform.SetParent(baseLayer.transform);
                newTile.transform.localScale = new Vector3(newTile.transform.localScale.x, newTile.transform.localScale.y * 0.6f, newTile.transform.localScale.z);
                targetTile.GameObject = newTile;
            }
        }
    }

    // Fire
    private void ChangeTreeTileToNothing(List<DefaultTile> pathNodes)
    {
        List<GameObject> tiles = GetChildObjects(obstacleLayer.GetComponent<Tilemap>());

        foreach (DefaultTile pn in pathNodes)
        {
            DefaultTile tempTile = WorldController.Instance.ObstacleLayer.Where(t => t.XPos == pn.XPos && t.YPos == pn.YPos).FirstOrDefault();

            if (tempTile != null && tempTile.GameObject != null && tempTile.GameObject.name.ToLower().Contains("tree"))
            {
                WorldController.Instance.BaseLayer.Where(t => t.XPos == pn.XPos && t.YPos == pn.YPos).FirstOrDefault().Walkable = true;
                WorldController.Instance.ObstacleLayer.Where(t => t.XPos == pn.XPos && t.YPos == pn.YPos).FirstOrDefault();
                Destroy(tempTile.GameObject);
                tempTile.GameObject = null;
            }
        }
    }

    // Fire
    private void ChangeBridgeTileToWater(List<DefaultTile> pathNodes)
    {
        List<GameObject> tiles = GetChildObjects(baseLayer.GetComponent<Tilemap>());

        foreach (DefaultTile pn in pathNodes)
        {
            GameObject obj = FindNearestGameObjectTile(pn, tiles);
            if (obj.name.ToLower().Contains("bridge"))
            {
                DefaultTile targetTile = WorldController.Instance.BaseLayer.Where(t => t.XPos == pn.XPos && t.YPos == pn.YPos).FirstOrDefault();
                WorldController.Instance.BaseLayer.Where(t => t.XPos == pn.XPos && t.YPos == pn.YPos).FirstOrDefault().Walkable = false;
                GameObject newTile = GameObject.Instantiate(waterTile);
                newTile.transform.position = obj.transform.position;
                Destroy(obj);
                newTile.transform.SetParent(baseLayer.transform);
                newTile.transform.localScale = new Vector3(newTile.transform.localScale.x, newTile.transform.localScale.y * 0.6f, newTile.transform.localScale.z);
                targetTile.GameObject = newTile;
            }
        }
    }

    private List<GameObject> GetChildObjects(Tilemap tilemap)
    {
        List<GameObject> tiles = new List<GameObject>();
        for (int i = 0; i < tilemap.transform.childCount; i++)
        {
            GameObject child = tilemap.transform.GetChild(i).gameObject;
            tiles.Add(child);
        }

        return tiles;
    }

    private GameObject FindNearestGameObjectTile(DefaultTile pn, List<GameObject> tiles)
    {
        GameObject tileObject = tiles.OrderBy(t => Math.Abs(pn.GameObject.transform.position.x - t.transform.position.x)).ThenBy(t => Math.Abs(pn.GameObject.transform.position.z - t.transform.position.z)).ToList().FirstOrDefault();
        return tileObject;
    }
}
