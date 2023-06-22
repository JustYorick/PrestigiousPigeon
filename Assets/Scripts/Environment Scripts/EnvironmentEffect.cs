using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using ReDesign;
using ReDesign.Entities;

namespace ReDesign{
public class EnvironmentEffect : MonoBehaviour
{
    public ParticleSystem particleSystem;
    // Tiles
    [SerializeField] private GameObject iceTile;
    [SerializeField] private GameObject waterTile;
    [SerializeField] private GameObject treeTile;
    [SerializeField] private GameObject bridgeTile;
    [SerializeField] private GameObject puddleTile;
    [SerializeField] private GameObject FrozenPillar;
    
    [SerializeField] private AudioClip treeBurnSound;

    
    /// <summary>
    /// Makes all relevant tiles react to fire environment effect
    /// </summary>
    /// <param name="pathNodes">List of nodes from the pathNodesMap that are in the range of the spell/effect used</param>
    public void FireEnvironmentEffects(List<DefaultTile> pathNodes)
    {
        ChangeIceTilesToWater(pathNodes);
        ChangeBridgeTileToWater(pathNodes);
        ChangeTreeTileToNothing(pathNodes);
        ChangeFrozenPillarTilesToPuddle(pathNodes);
    }

    /// <summary>
    /// Makes all relevant tiles react to ice environment effect
    /// </summary>
    /// <param name="pathNodes">List of nodes from the pathNodesMap that are in the range of the spell/effect used</param>
    public void IceEnvironmentEffects(List<DefaultTile> pathNodes)
    {
        ChangeWaterTilesToIce(pathNodes);
        CastIceOnObelisk(pathNodes);
        ChangePuddleToFrozenPillar(pathNodes);
    }
    
    /// <summary>
    /// Makes all relevant tiles react to water environment effect
    /// </summary>
    /// <param name="pathNodes">List of nodes from the pathNodesMap that are in the range of the spell/effect used</param>
    public void WaterEnvironmentEffects(List<DefaultTile> pathNodes)
    {
        TrySpawningPuddle(pathNodes);
        ChangePillarToNothing(pathNodes);
        ChangeGraveToNothing(pathNodes);
    }

    // Ice
    public void ChangeWaterTilesToIce(List<DefaultTile> pathNodes)
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
    
    // Ice
    private void ChangePuddleToFrozenPillar(List<DefaultTile> pathNodes)
    {
        List<DefaultTile> obstacleLayer = WorldController.ObstacleLayer;
        foreach (DefaultTile pn in pathNodes)
        {
            DefaultTile tempTile = WorldController.ObstacleLayer.Where(t => t.XPos == pn.XPos && t.YPos == pn.YPos).FirstOrDefault();

            if (tempTile != null && tempTile.GameObject != null && tempTile.GameObject.name.ToLower().Contains("puddle"))
            {
                DefaultTile targetTile = WorldController.Instance.BaseLayer.Where(t => t.XPos == pn.XPos && t.YPos == pn.YPos).FirstOrDefault();
                
                GameObject objObs = FindExactGameObjectTile(pn, obstacleLayer);
                GameObject newTile = Instantiate(FrozenPillar);
                newTile.AddComponent<FrozenPillar>();
                newTile.name = "frozenpillar";
                var position = objObs.transform.position;
                newTile.transform.localScale = new Vector3(0.5f, 0.2f, 0.2f);
                newTile.transform.position = position;
                targetTile.Walkable = false;
                targetTile.GameObject = newTile;
                
                WorldController.Instance.BaseLayer.Where(t => t.XPos == pn.XPos && t.YPos == pn.YPos).FirstOrDefault().Walkable = false;

                WorldController.ObstacleLayer.Remove(WorldController.ObstacleLayer.Where(t => t.XPos == pn.XPos && t.YPos == pn.YPos).FirstOrDefault());
                Destroy(tempTile.GameObject);
                tempTile.GameObject = null;
            }
        }
    }

    // Ice
    private void CastIceOnObelisk(List<DefaultTile> pathNodes)
    {
        foreach (DefaultTile pn in pathNodes)
        {
            DefaultTile tempTile = WorldController.ObstacleLayer.Where(t => t.XPos == pn.XPos && t.YPos == pn.YPos).FirstOrDefault();

            if (tempTile != null && tempTile.GameObject != null && tempTile.GameObject.name.ToLower().Contains("obelisk"))
            {

                for (int i = -4; i < 5; i++){
                    for (int j = -4; j < 5; j++){
                        DefaultTile enemyTile = WorldController.ObstacleLayer.Where(t => t.XPos == pn.XPos+j && t.YPos == pn.YPos+i).FirstOrDefault();
                        if (enemyTile != null && enemyTile.GameObject != null && enemyTile.GameObject.CompareTag("Entity") && !enemyTile.GameObject.name.Contains("Player"))
                        {

                            GameObject p = GameObject.Find("Ice Particles");
                            if (p != null)
                                {
                                    Vector3 pos = enemyTile.GameObject.transform.position + new Vector3(0,-1,0);
                                    particleSystem = Instantiate(p.GetComponent<ParticleSystem>(), pos, Quaternion.Euler(-90, 0, 0));
                                    particleSystem.Play();
                                }

                            Entity enemy = enemyTile.GameObject.GetComponent<Entity>();
                            enemy.ReceiveDamage(5);
                        }
                    }
                }
            }
        }
    }

    // Ice
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

    // Ice
    private void ChangeGraveToNothing(List<DefaultTile> pathNodes)
    {
        foreach (DefaultTile pn in pathNodes)
        {
            DefaultTile tempTile = WorldController.ObstacleLayer.Where(t => t.XPos == pn.XPos && t.YPos == pn.YPos).FirstOrDefault();

            if (tempTile != null && tempTile.GameObject != null && tempTile.GameObject.name.ToLower().Contains("grave"))
            {
                WorldController.Instance.BaseLayer.Where(t => t.XPos == pn.XPos && t.YPos == pn.YPos).FirstOrDefault().Walkable = true;

                WorldController.ObstacleLayer.Remove(WorldController.ObstacleLayer.Where(t => t.XPos == pn.XPos && t.YPos == pn.YPos).FirstOrDefault());
                Destroy(tempTile.GameObject);
                tempTile.GameObject = null;

                //spawn skeleton (?)
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

                // Entities around burning tree take damage
                for (int i = -1; i < 2; i++){
                    for (int j = -1; j < 2; j++){
                        DefaultTile enemyTile = WorldController.ObstacleLayer.Where(t => t.XPos == pn.XPos+j && t.YPos == pn.YPos+i).FirstOrDefault();
                        if (enemyTile != null && enemyTile.GameObject != null && enemyTile.GameObject.CompareTag("Entity") && !enemyTile.GameObject.name.Contains("Player"))
                        {
                            GameObject p = GameObject.Find("Fire Particles");
                            if (p != null)
                                {
                                    Vector3 pos = enemyTile.GameObject.transform.position + new Vector3(0,-1,0);
                                    particleSystem = Instantiate(p.GetComponent<ParticleSystem>(), pos, Quaternion.Euler(-90, 0, 0));
                                    particleSystem.Play();
                                }
                            Entity enemy = enemyTile.GameObject.GetComponent<Entity>();
                            enemy.ReceiveDamage(5);
                        }
                    }
                }

                
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
    
    // Fire
    public void ChangeFrozenPillarTilesToPuddle(List<DefaultTile> pathNodes)
    {
        List<DefaultTile> tiles = WorldController.Instance.BaseLayer;
        foreach (DefaultTile pn in pathNodes)
        {
            GameObject obj = FindExactGameObjectTile(pn, tiles);

            if (obj.name.ToLower().Contains("frozenpillar"))
            {
                DefaultTile targetTile = WorldController.Instance.BaseLayer.Where(t => t.XPos == pn.XPos && t.YPos == pn.YPos).FirstOrDefault();
                WorldController.Instance.BaseLayer.Where(t => t.XPos == pn.XPos && t.YPos == pn.YPos).FirstOrDefault().Walkable = false;
                GameObject newTile = Instantiate(puddleTile);
                newTile.transform.position = obj.transform.position;
                Destroy(obj);
                newTile.transform.localScale = new Vector3(newTile.transform.localScale.x, newTile.transform.localScale.y * 0.5001f, newTile.transform.localScale.z);
                targetTile.GameObject = newTile;
                WorldController.Instance.addObstacle(newTile, true);
            }
        }
    }
    
    // Water
    private void TrySpawningPuddle(List<DefaultTile> pathNodes)
    {
        List<DefaultTile> obstacleLayer = WorldController.ObstacleLayer;
        List<DefaultTile> baseLayer = WorldController.Instance.BaseLayer;
        
        foreach (var tile in pathNodes)
        {
            // Debug.Log(tile);
            // Debug.Log(obstacleLayer);
            // Debug.Log(baseLayer);
            GameObject objObs = FindExactGameObjectTile(tile, obstacleLayer);
            GameObject objBase = FindExactGameObjectTile(tile, baseLayer);
            if (!objObs && (objBase.name.ToLower().Contains("grass") || objBase.name.ToLower().Contains("gravel")))
            {
                GameObject puddle = Instantiate(puddleTile);
                Vector3 pos = tile.GameObject.transform.position;
                puddle.transform.position = new Vector3(pos.x, pos.y+0.5001f, pos.z);
                WorldController.Instance.addObstacle(puddle, true); 
            }
        }
    }


    private GameObject FindExactGameObjectTile(DefaultTile pn, List<DefaultTile> tiles)
    {
        GameObject tileObject = tiles.Where(t => pn.XPos == t.XPos && pn.YPos == t.YPos).FirstOrDefault()?.GameObject;
        
        return tileObject;
    }
}
}
