using ReDesign.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ReDesign
{
    public abstract class AttacksAndSpells
    {
        public abstract int MinimumRange { get; }
        public abstract int MaximumRange { get; }
        public abstract int Damage { get; }

        /// <summary>
        /// Causes the spell or attack to be casted at targeted x and y coordinates
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void Effect(int x, int y)
        {
            DefaultTile targetTile = WorldController.Instance.BaseLayer.Where(t => t.XPos == x && t.YPos == y).FirstOrDefault(); // change to GetTile()?
            List<DefaultTile> tileList = new List<DefaultTile>() { targetTile };

            DefaultTile enemyTile = WorldController.ObstacleLayer.Where(t => t.XPos == x && t.YPos == y).FirstOrDefault();

            if (enemyTile != null && enemyTile.GameObject != null && enemyTile.GameObject.CompareTag("Entity"))
            {
                Entity enemy = enemyTile.GameObject.GetComponent<Entity>();
                Debug.Log("dmg" + Damage);
                enemy.ReceiveDamage(Damage);
            } 
            else
            {
                EnvironmentEffect(tileList);
            }
            
            //foreach enemy/tile
            //mana -2
            //range alle tiles met afstand van 2
            //apply damage
        }

        /// <summary>
        /// Makes environment react to spell. This gets called in the Effect(..) method.
        /// </summary>
        /// <param name="targetTiles">List of tiles that are affected by the environment effect</param>
        public abstract void EnvironmentEffect(List<DefaultTile> targetTiles);

        /// <summary>
        /// Use this to determine tiles the spell can hit
        /// </summary>
        /// <param name="x">Player position simplified x coordinate</param>
        /// <param name="y">Player position simplified y coordinate</param>
        /// <returns>List of possible locations the spell can target when casted from given location</returns>
        public List<DefaultTile> GetTargetLocations(int x, int y)
        {
            List<DefaultTile> locations = new List<DefaultTile>();
            for (int moveX = -MaximumRange; moveX <= MaximumRange; moveX++)
            {
                for (int moveY = -MaximumRange; moveY <= MaximumRange; moveY++)
                {
                    if (Math.Abs(moveX) + Math.Abs(moveY) <= MaximumRange && Math.Abs(moveX) + Math.Abs(moveY) >= MinimumRange)
                    {
                        int targetLocationX = x + moveX;
                        int targetLocationY = y + moveY;
                        DefaultTile possibleTile = WorldController.Instance.BaseLayer.Where(t => t.XPos == targetLocationX && t.YPos == targetLocationY).FirstOrDefault();
                        if (possibleTile != null)
                            locations.Add(possibleTile);
                    }
                }
            }

            return locations;
        }
    }
}
