using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReDesign
{
    public class LichAttack : AttacksAndSpells
    {
        public override int MinimumRange { get { return 1; } }

            public override int MaximumRange { get { return 2; } }

            public override int Damage { get { return 4; } }

            public override void EnvironmentEffect(List<DefaultTile> targetTiles)
            {
                //throw new System.NotImplementedException();
            }
    }
}
