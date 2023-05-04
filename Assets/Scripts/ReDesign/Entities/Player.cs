using System;
using System.Collections.Generic;
using UnityEngine;

namespace ReDesign.Entities
{
    public class Player : Entity
    {
        [SerializeField] private ManaSystem _manaSystem;
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

        public override void Update()
        {
            if (StateController.currentState == GameState.PlayerTurn)
            {
                RangeTileTool.Instance.drawMoveRange(WorldController.getPlayerTile(), _manaSystem.GetMana());
            }
        }

        public override void NextAction()
        {
            StateController.ChangeState(GameState.PlayerTurn);
            Debug.Log("im a player");
            //StateController.ChangeState(GameState.EndTurn);
            _manaSystem.StartTurn();
        }

            
        public override void Move()
        {
            //throw new System.NotImplementedException();
        }

        public override void Attack()
        {
            attacking = false;
        }

        public void EndTurn() => StateController.ChangeState(GameState.EndTurn);

        public override void ReceiveDamage(int dmg)
        {
            base.ReceiveDamage(dmg);
            PlayerAnimator._animator.SetBool("isHit", true);
            PlayerAnimator._animator.SetBool("isIdle", false);
        }
    }
}