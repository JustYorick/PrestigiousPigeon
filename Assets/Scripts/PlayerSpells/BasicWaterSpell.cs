using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ReDesign;

public class BasicWaterSpell : AttacksAndSpells
{
    public override int MinimumRange { get { return 1; } }
    public override int MaximumRange { get { return 3; } }
    public override int Damage { get { return 1; } }
    public override int ManaCost { get { return 1; } }
    public override string attackName { get{ return "Water spell"; } }

    public override void EnvironmentEffect(List<DefaultTile> targetTiles)
    {
        WorldController.Instance.GetComponent<EnvironmentEffect>().WaterEnvironmentEffects(targetTiles);
    }
}
