using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ReDesign;

public class BasicIceSpell : AttacksAndSpells
{
    public override int MinimumRange { get { return 2; } }
    public override int MaximumRange { get { return 2; } }
    public override int Damage { get { return 5; } } //Change back to 5
    public override int ManaCost { get { return 2; } }

    public override void EnvironmentEffect(List<DefaultTile> targetTiles)
    {
        WorldController.Instance.GetComponent<EnvironmentEffect>().IceEnvironmentEffects(targetTiles);
    }
}
