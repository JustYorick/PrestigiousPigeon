using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReDesign
{
    public class SkeletonAttack : AttacksAndSpells
    {
        public override int MinimumRange { get { return 2; } }

            public override int MaximumRange { get { return 3; } }

            public override int Damage { get { return 3; } }

            public override void EnvironmentEffect(List<DefaultTile> targetTiles)
            {
                //throw new System.NotImplementedException();
            }
    }
}
