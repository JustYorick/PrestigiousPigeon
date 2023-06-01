using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ReDesign.Entities
{
    public class SnowBoss : Entity
    {
        public override int SightRange { get { return 30; } }
        public override int MoveRange { get { return 2; } }
        
        public SnowBoss()
        {
            int MaxHealth = 25;
            _entityHealth = new UnitHealth(MaxHealth, MaxHealth);
            Attacks = new List<AttacksAndSpells>()
            {
                new SnowBossAttack()
            };
        }

        public override void NextAction()
        {
            //Debug.Log("im a SnowBoss");
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
                MoveToPlayer(this.MoveRange);
            } else
            {
                MoveToPlayer(0);
            }
            
            //foreach(AttacksAndSpells atk in _attacks)
        }

        public override void Attack()
        {
            DefaultTile currentTile = WorldController.ObstacleLayer.Where(o => o.GameObject == this.gameObject).FirstOrDefault();
            List<DefaultTile> targetTiles = Attacks[0].GetTargetLocations(currentTile.XPos, currentTile.YPos);
            DefaultTile targetTile = targetTiles.Where(t => t.XPos == WorldController.getPlayerTile().XPos && t.YPos == WorldController.getPlayerTile().YPos).FirstOrDefault();
            if (targetTile != null)
            {
                StartCoroutine(EnemyRotateToAttack());
                Attacks[0].Effect(targetTile.XPos, targetTile.YPos);
            }
            attacking = false;
            StopCoroutine(EnemyRotateToAttack());
        }
    }
}