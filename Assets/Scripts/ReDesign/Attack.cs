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
        private int Damage = 10;
        private int Range = 2;
        private SpellEffect SpellEffect = new Freeze();
    }
}