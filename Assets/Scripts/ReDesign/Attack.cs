using UnityEngine.SocialPlatforms;

namespace ReDesign
{
    public abstract class Attack
    {
        private int Damage { get; }
        private int Range { get; }
        private SpellEffect SpellEffect { get; }
    }

    public class Frostbolt : Attack
    {
        private int Damage = 5;
        private int Range = 2;
        private SpellEffect SpellEffect = new Freeze();
    }

    public class Fireball : Attack
    {
        private int Damage = 5;
        private int Range = 2;
        private SpellEffect SpellEffect = new Burn();
    }

    public class SlimeAttack : Attack
    {
        private int Damage = 2;
        private int Range = 1;
    }
}