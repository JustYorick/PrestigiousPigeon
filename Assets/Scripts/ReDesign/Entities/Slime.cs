using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ReDesign.Entities
{
    public class Slime : Entity
    {
        
        public Slime()
        {
            int MaxHealth = 5;
            _entityHealth = new UnitHealth(MaxHealth, MaxHealth);
            Attacks = new List<AttacksAndSpells>()
            {
                new SlimeAttack()
            };
        }

        //private List<AttacksAndSpells> Attacks = new List<AttacksAndSpells>()
        //{
        //    new SlimeAttack()
        //};

        public override void NextAction()
        {
            Debug.Log("im a slime");
            StateController.ChangeState(GameState.EnemyTurn);

            Move();
            //Attack();
            
        }

        public override void Move()
        {
            MoveToPlayer(1);
            //foreach(AttacksAndSpells atk in _attacks)
            
        }

        public override void Attack()
        {
            DefaultTile currentTile = WorldController.ObstacleLayer.Where(o => o.GameObject == this.gameObject).FirstOrDefault();
            List<DefaultTile> targetTiles = Attacks[0].GetTargetLocations(currentTile.XPos, currentTile.YPos);
            DefaultTile targetTile = targetTiles.Where(t => t.XPos == WorldController.getPlayerTile().XPos && t.YPos == WorldController.getPlayerTile().YPos).FirstOrDefault();
            if (targetTile != null)
            {
                Debug.Log("targettile");
                Attacks[0].Effect(targetTile.XPos, targetTile.YPos);
            }
            attacking = false;
            
            //_attacks[0].Effect(WorldController.getPlayerTile().XPos, WorldController.getPlayerTile().YPos);
        }
    }
}