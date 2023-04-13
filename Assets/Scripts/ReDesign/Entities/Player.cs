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
            StateController.ChangeState(GameState.PlayerTurn);
            Debug.Log("im a player");
            //StateController.ChangeState(GameState.EndTurn);

            List<DefaultTile> locations = _attacks[0].GetTargetLocations(WorldController.getPlayerTile().XPos, WorldController.getPlayerTile().YPos);
            foreach (var defaultTile in locations)
            {
                RangeTileTool.Instance.SpawnTile(defaultTile.XPos, defaultTile.YPos, new Color(255,0,0, 0.5f));
            }
        }

        public override void Move()
        {
            //throw new System.NotImplementedException();
        }

        public override void Attack()
        {
            attacking = false;
        }
    }
}