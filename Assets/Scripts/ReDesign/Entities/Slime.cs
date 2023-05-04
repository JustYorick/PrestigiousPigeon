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
        // private Vector3 targetLoc;
        
        public Slime()
        {
            int MaxHealth = 5;
            _entityHealth = new UnitHealth(MaxHealth, MaxHealth);
            Attacks = new List<AttacksAndSpells>()
            {
                new SlimeAttack()
            };
        }
        //
        // public virtual void Awake()
        // {
        //     targetLoc = transform.position;
        // }

        // public new void Update()
        // {
        //     RotateEntity();
        // }
        
        public override void NextAction()
        {
            //Debug.Log("im a slime");
            StateController.ChangeState(GameState.EnemyTurn);

            //Move() will call Attack() and change turn
            Move();

        }

        public override void Move()
        {
            DefaultTile currentTile = WorldController.ObstacleLayer.Where(o => o.GameObject == this.gameObject).FirstOrDefault();
            DefaultTile enemyPos = WorldController.getPlayerTile();
            // targetLoc = new Vector3(enemyPos.GameObject.transform.position.x, enemyPos.GameObject.transform.position.y, enemyPos.GameObject.transform.position.z);
            int range = Math.Abs(currentTile.XPos - enemyPos.XPos) + Math.Abs(currentTile.YPos - enemyPos.YPos);
            Debug.Log(""+range);
            if (range < 9)
            {
                MoveToPlayer(1);
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
                //Debug.Log("targettile");
                Attacks[0].Effect(targetTile.XPos, targetTile.YPos);
            }
            attacking = false;
        }
    }
}