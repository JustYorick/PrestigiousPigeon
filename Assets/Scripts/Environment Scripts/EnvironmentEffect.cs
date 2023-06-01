using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using ReDesign;

public class EnvironmentEffect : MonoBehaviour
{
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
        ChangePillarToNothing(pathNodes);
    }

    // Ice
    private void ChangeWaterTilesToIce(List<DefaultTile> pathNodes)
    {
        List<DefaultTile> tiles = WorldController.Instance.BaseLayer;
        foreach (DefaultTile pn in pathNodes)
        {
            GameObject obj = FindExactGameObjectTile(pn, tiles);
            if (obj.name.ToLower().Contains("water"))
            {
                DefaultTile targetTile = WorldController.Instance.BaseLayer.Where(t => t.XPos == pn.XPos && t.YPos == pn.YPos).FirstOrDefault();
                targetTile.Walkable = true;
                GameObject newTile = GameObject.Instantiate(iceTile);
                newTile.transform.position = obj.transform.position;
                Destroy(obj);
                targetTile.GameObject = newTile;
            }
        }
    }

    private void ChangePillarToNothing(List<DefaultTile> pathNodes)
    {
        foreach (DefaultTile pn in pathNodes)
        {
            DefaultTile tempTile = WorldController.ObstacleLayer.Where(t => t.XPos == pn.XPos && t.YPos == pn.YPos).FirstOrDefault();

            if (tempTile != null && tempTile.GameObject != null && tempTile.GameObject.name.ToLower().Contains("pillar"))
            {
                //WorldController.Instance.BaseLayer.Where(t => t.XPos == pn.XPos && t.YPos == pn.YPos).FirstOrDefault().Walkable = true;

                WorldController.ObstacleLayer.Remove(WorldController.ObstacleLayer.Where(t => t.XPos == pn.XPos && t.YPos == pn.YPos).FirstOrDefault());
                Destroy(tempTile.GameObject);
                tempTile.GameObject = null;
            }
        }
    }

    // Fire
    private void ChangeIceTilesToWater(List<DefaultTile> pathNodes)
    {
        List<DefaultTile> tiles = WorldController.Instance.BaseLayer;
        foreach (DefaultTile pn in pathNodes)
        {
            GameObject obj = FindExactGameObjectTile(pn, tiles);
            
            if (obj.name.ToLower().Contains("ice"))
            {
                DefaultTile targetTile = WorldController.Instance.BaseLayer.Where(t => t.XPos == pn.XPos && t.YPos == pn.YPos).FirstOrDefault();
                WorldController.Instance.BaseLayer.Where(t => t.XPos == pn.XPos && t.YPos == pn.YPos).FirstOrDefault().Walkable = false;
                GameObject newTile = GameObject.Instantiate(waterTile);
                newTile.transform.position = obj.transform.position;
                Destroy(obj);
                newTile.transform.localScale = new Vector3(newTile.transform.localScale.x, newTile.transform.localScale.y * 0.6f, newTile.transform.localScale.z);
                targetTile.GameObject = newTile;
            }
        }
    }

    // Fire
    private void ChangeTreeTileToNothing(List<DefaultTile> pathNodes)
    {
        foreach (DefaultTile pn in pathNodes)
        {
            DefaultTile tempTile = WorldController.ObstacleLayer.Where(t => t.XPos == pn.XPos && t.YPos == pn.YPos).FirstOrDefault();

            if (tempTile != null && tempTile.GameObject != null && tempTile.GameObject.name.ToLower().Contains("tree"))
            {
                WorldController.Instance.BaseLayer.Where(t => t.XPos == pn.XPos && t.YPos == pn.YPos).FirstOrDefault().Walkable = true;

                WorldController.ObstacleLayer.Remove(WorldController.ObstacleLayer.Where(t => t.XPos == pn.XPos && t.YPos == pn.YPos).FirstOrDefault());
                Destroy(tempTile.GameObject);
                tempTile.GameObject = null;
            }
        }
    }

    // Fire
    private void ChangeBridgeTileToWater(List<DefaultTile> pathNodes)
    {
        List<DefaultTile> tiles = WorldController.Instance.BaseLayer;
        foreach (DefaultTile pn in pathNodes)
        {
            GameObject obj = FindExactGameObjectTile(pn, tiles);
            if (obj.name.ToLower().Contains("bridge"))
            {
                DefaultTile targetTile = WorldController.Instance.BaseLayer.Where(t => t.XPos == pn.XPos && t.YPos == pn.YPos).FirstOrDefault();
                WorldController.Instance.BaseLayer.Where(t => t.XPos == pn.XPos && t.YPos == pn.YPos).FirstOrDefault().Walkable = false;
                GameObject newTile = GameObject.Instantiate(waterTile);
                newTile.transform.position = obj.transform.position;
                Destroy(obj);
                newTile.transform.localScale = new Vector3(newTile.transform.localScale.x, newTile.transform.localScale.y * 0.6f, newTile.transform.localScale.z);
                targetTile.GameObject = newTile;
            }
        }
    }

    private GameObject FindExactGameObjectTile(DefaultTile pn, List<DefaultTile> tiles)
    {
        GameObject tileObject = tiles.Where(t => pn.XPos == t.XPos && pn.YPos == t.YPos).FirstOrDefault().GameObject;
        return tileObject;
    }
}
