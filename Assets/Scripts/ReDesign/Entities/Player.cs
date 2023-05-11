using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReDesign.Entities
{
    public class Player : Entity
    {
        [SerializeField] private ManaSystem _manaSystem;
        
        public override int SightRange { get; }
        public override int MoveRange { get { return _manaSystem.GetMana(); } }
        
        private List<AttacksAndSpells> _attacks = new List<AttacksAndSpells>
        {
            new BasicFireSpell(),
            new BasicIceSpell()
        };

        public virtual void Awake()
        {
            int MaxHealth = 20;
            _entityHealth = new UnitHealth(MaxHealth, MaxHealth);
        }

        private void Start()
        {
            RangeTileTool.Instance.drawMoveRange(WorldController.getPlayerTile(), _manaSystem.GetMana());
        }

        public override void Update()
        {
            //Player needs to override update since the entity update will end the players turn at the en of movement.
        }

        public override void NextAction()
        {
            StateController.ChangeState(GameState.PlayerTurn);
            Debug.Log("im a player");
            //StateController.ChangeState(GameState.EndTurn);
            _manaSystem.StartTurn();
            RangeTileTool.Instance.drawMoveRange(WorldController.getPlayerTile(), _manaSystem.GetMana());
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