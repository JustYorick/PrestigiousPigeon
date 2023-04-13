using System.Collections.Generic;
using UnityEngine.SocialPlatforms;

namespace ReDesign
{
    public class SlimeAttack : AttacksAndSpells
    {
        public override int MinimumRange { get { return 1; } }

        public override int MaximumRange { get { return 1; } }

        public override int Damage { get { return 2; } }

        public override void EnvironmentEffect(List<DefaultTile> targetTiles)
        {
            //throw new System.NotImplementedException();
        }
    }
}