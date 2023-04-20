using System.Collections.Generic;
using ReDesign;

namespace PlayerSpells
{
    public class BasicIceSpell : AttacksAndSpells
    {
        public override int MinimumRange { get { return 2; } }
        public override int MaximumRange { get { return 2; } }
        public override int Damage { get { return 5; } } 
        public override int ManaCost { get { return 2; } }

        public override void EnvironmentEffect(List<DefaultTile> targetTiles)
        {
            WorldController.Instance.GetComponent<EnvironmentEffect>().IceEnvironmentEffects(targetTiles);
        }
    }
}
