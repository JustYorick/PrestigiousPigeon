using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ReDesign
{
    public abstract class SpellEffect
    {
        public abstract int MinimumRange { get; }
        public abstract int MaximumRange { get; }

        /// <summary>
        /// Causes the spell to be casted at targeted x and y coordinates
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public abstract void Effect(int x, int y);

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
                    if (Math.Abs(moveX) + Math.Abs(moveY) == MaximumRange)
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

        public List<DefaultTile> GetTargetLocations(int[][] coordinates) //Or something similar for AOE spells
        {
            return null;
        }
    }

    public class Burn : SpellEffect
    {
        public override void Effect()
        {
            throw new NotImplementedException();
        } 
    }
}
