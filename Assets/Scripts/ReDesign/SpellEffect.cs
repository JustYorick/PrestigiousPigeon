using System;
using UnityEngine;

namespace ReDesign
{
    public abstract class SpellEffect
    {
        public abstract void Effect();
    }

    public class Freeze : SpellEffect
    {
        public override void Effect()
        {
            throw new System.NotImplementedException();
        }
    }

    public class Burn : SpellEffect
    {
        public override void Effect()
        {
            throw new NotImplementedException();
        } 
    }
}
