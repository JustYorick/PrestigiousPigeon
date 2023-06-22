using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ReDesign.Entities
{
    public class Lich : Enemy
    {
        public override int SightRange { get { return 6; } }
        public override int MoveRange { get { return 2; } }

        private EntityAnimator _lichAnimator;
        public override string displayName{ get { return "Lich"; } }

        private void Awake()
        {
            _lichAnimator = GetComponentInChildren<EntityAnimator>();

        }

        public Lich()
        {
            int MaxHealth = 8;
            _entityHealth = new UnitHealth(MaxHealth, MaxHealth);
            Attacks = new List<AttacksAndSpells>()
            {
                new LichAttack()
            };
        }

        public override void NextAction()
        {
            StateController.ChangeState(GameState.EnemyTurn);

            //Move() will call Attack() and change turn
            Move();
            
        }

        public override void Move()
        {
            DefaultTile currentTile = WorldController.ObstacleLayer.Where(o => o.GameObject == this.gameObject).FirstOrDefault();
            DefaultTile enemyPos = WorldController.getPlayerTile();
            int range = Math.Abs(currentTile.XPos - enemyPos.XPos) + Math.Abs(currentTile.YPos - enemyPos.YPos);
            if (range < SightRange)
            {
                MoveToPlayer(this.MoveRange, _lichAnimator);
            } else
            {
                MoveToPlayer(0);
            }
            
        }

        public override void Attack(AudioClip attackSound)
        {
            DefaultTile currentTile = WorldController.ObstacleLayer.Where(o => o.GameObject == this.gameObject).FirstOrDefault();
            List<DefaultTile> targetTiles = Attacks[0].GetTargetLocations(currentTile.XPos, currentTile.YPos);
            DefaultTile targetTile = targetTiles.Where(t => t.XPos == WorldController.getPlayerTile().XPos && t.YPos == WorldController.getPlayerTile().YPos).FirstOrDefault();
            if (targetTile != null)
            {
                _lichAnimator.SetAttacking();
                StartCoroutine(EnemyRotateToAttack());
                SoundManager.Instance.PlaySound(attackSound);

                Attacks[0].Effect(targetTile.XPos, targetTile.YPos);
            }
            //attacking = false;
            StopCoroutine(EnemyRotateToAttack());
        }
    }
}