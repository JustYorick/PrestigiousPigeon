using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UIElements;

namespace ReDesign.Entities
{
    public class Slime : Entity
    {
        public override int SightRange { get { return 9; } }
        public override int MoveRange { get { return 1; } }
        private EntityAnimator _slimeAnimator;

        private void Awake()
        {
            _slimeAnimator = GetComponentInChildren<EntityAnimator>();
        }
        
        public Slime()
        {
            int MaxHealth = 5;
            _entityHealth = new UnitHealth(MaxHealth, MaxHealth);
            Attacks = new List<AttacksAndSpells>()
            {
                new SlimeAttack()
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
            DefaultTile currentTile = WorldController.ObstacleLayer.Where(o => o.GameObject == this.gameObject)
                .FirstOrDefault();
            DefaultTile enemyPos = WorldController.getPlayerTile();
            int range = Math.Abs(currentTile.XPos - enemyPos.XPos) + Math.Abs(currentTile.YPos - enemyPos.YPos);
            Debug.Log("" + range);
            if (range < SightRange)
            {
                MoveToPlayer(this.MoveRange, _slimeAnimator);
            }
            else
            {
                MoveToPlayer(0);
            }
        }

        public override void Attack()
        {
            DefaultTile currentTile = WorldController.ObstacleLayer.Where(o => o.GameObject == this.gameObject)
                .FirstOrDefault();
            List<DefaultTile> targetTiles = Attacks[0].GetTargetLocations(currentTile.XPos, currentTile.YPos);
            DefaultTile targetTile = targetTiles.Where(t =>
                    t.XPos == WorldController.getPlayerTile().XPos && t.YPos == WorldController.getPlayerTile().YPos)
                .FirstOrDefault();
            if (targetTile != null)
            {
                StartCoroutine(EnemyRotateToAttack());
                _slimeAnimator.SetAttacking();
                Attacks[0].Effect(targetTile.XPos, targetTile.YPos);
            }

            StopCoroutine(EnemyRotateToAttack());
        }
        
    }
}