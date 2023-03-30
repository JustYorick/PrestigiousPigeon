using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ReDesign;
using System.Linq;

public class BasicIceSpell : SpellEffect
{
    public override int MinimumRange { get { return 2; } }
    public override int MaximumRange { get { return 2; } }

    public override void Effect(int x, int y)
    {
        DefaultTile targetTile = WorldController.Instance.BaseLayer.Where(t => t.XPos == x && t.YPos == y).FirstOrDefault(); // change to GetTile()?
        List<DefaultTile> tileList = new List<DefaultTile>() { targetTile };
        WorldController.Instance.GetComponent<EnvironmentEffect>().IceEnvironmentEffects(tileList);
        //foreach enemy/tile
        //mana -2
        //range alle tiles met afstand van 2
        //apply damage
    }
}
