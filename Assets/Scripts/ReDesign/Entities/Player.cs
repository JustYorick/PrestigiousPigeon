using System;
using System.Collections.Generic;
using UnityEngine;

namespace ReDesign.Entities
{
    public class Player : Entity
    {

        private List<AttacksAndSpells> _attacks = new List<AttacksAndSpells>
        {
            new BasicFireSpell(),
            new BasicIceSpell()
        };

        private void Awake()
        {
            int MaxHealth = 20;
            _entityHealth = new UnitHealth(MaxHealth, MaxHealth);
        }

        public override void NextAction()
        {
            //do sum stuff
            TurnController.ResolveTurn();
        }

        public override void Move()
        {
            throw new System.NotImplementedException();
        }

        public override void Attack()
        {
            throw new System.NotImplementedException();
        }
    }
}