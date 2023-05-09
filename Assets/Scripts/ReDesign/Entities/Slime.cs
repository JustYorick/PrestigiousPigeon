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
        private Vector3 targetLocation;
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
            if (range < 9)
            {
                MoveToPlayer(1);
            }
            else
            {
                MoveToPlayer(0);
            }
        }

        public override IEnumerator Attack()
        {
            DefaultTile currentTile = WorldController.ObstacleLayer.Where(o => o.GameObject == this.gameObject)
                .FirstOrDefault();
            List<DefaultTile> targetTiles = Attacks[0].GetTargetLocations(currentTile.XPos, currentTile.YPos);
            DefaultTile targetTile = targetTiles.Where(t =>
                    t.XPos == WorldController.getPlayerTile().XPos && t.YPos == WorldController.getPlayerTile().YPos)
                .FirstOrDefault();
            if (targetTile != null)
            {
                Vector3 slimePos = transform.position;
                Vector3 target = WorldController.getPlayerTile().GameObject.transform.position;
                GridLayout gr = WorldController.Instance.gridLayout;
                // Calculate the direction to the target position and set the entity's rotation accordingly
                Vector3 targetPos = new Vector3(target.x, slimePos.y, target.z);
                Vector3 dir = (targetPos - slimePos).normalized;
                Quaternion targetRotation = Quaternion.LookRotation(dir, Vector3.up);
                targetLocation = PlayerMovement.SnapCoordinateToGrid(targetPos, gr);
                float time = 0;

                // Loop until the entity has moved halfway to the target location
                while (time < 0.5f)
                {
                    // Adds the position and rotation
                    transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, time / 0.5f);
                    time += Time.deltaTime;
                    yield return attacking = false;
                }
                Attacks[0].Effect(targetTile.XPos, targetTile.YPos);
                transform.rotation = targetRotation;
            }
            attacking = false;
            yield return null;
        }
    }
}