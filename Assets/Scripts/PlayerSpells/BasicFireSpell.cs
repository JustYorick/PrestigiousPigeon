using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ReDesign;

public class BasicFireSpell : AttacksAndSpells
{
    public override int MinimumRange { get { return 2; } }
    public override int MaximumRange { get { return 2; } }
    public override int Damage { get { return 5; } }

    public override void EnvironmentEffect(List<DefaultTile> targetTiles)
    {
        WorldController.Instance.GetComponent<EnvironmentEffect>().FireEnvironmentEffects(targetTiles);
    }
}
