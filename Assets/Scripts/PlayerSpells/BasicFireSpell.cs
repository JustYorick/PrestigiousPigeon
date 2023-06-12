using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ReDesign;

public class BasicFireSpell : AttacksAndSpells
{
    public override int MinimumRange { get { return 2; } }
    public override int MaximumRange { get { return 2; } }
    public override int Damage { get { return 5; } }
    public override int ManaCost { get { return 2; } }
    private EnvironmentEffect environmentEffect;

    public BasicFireSpell(ParticleSystem particles){
        particleSystem = particles;
        environmentEffect = WorldController.Instance.GetComponent<EnvironmentEffect>();
    }

    public override void EnvironmentEffect(List<DefaultTile> targetTiles)
    {
        environmentEffect.FireEnvironmentEffects(targetTiles);
    }
}
