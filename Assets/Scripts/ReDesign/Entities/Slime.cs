using System.Collections.Generic;
using UnityEngine;

namespace ReDesign.Entities
{
    public class Slime : Entity
    {

        public Slime()
        {
            int MaxHealth = 5;
            _entityHealth = new UnitHealth(MaxHealth, MaxHealth);
        }

        private List<AttacksAndSpells> _attacks = new List<AttacksAndSpells>()
        {
            new SlimeAttack()
        };

        public override void NextAction()
        {
            StateController.ChangeState(GameState.EnemyTurn);
            Debug.Log("im a slime");
            StateController.ChangeState(GameState.EndTurn);
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