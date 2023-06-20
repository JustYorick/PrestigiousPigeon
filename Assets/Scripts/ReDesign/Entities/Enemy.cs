using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ReDesign.Entities
{
    public abstract class Enemy : Entity
    {
        [SerializeField] private GameObject _healthBar;
        [SerializeField] private GameObject deathEntityObject;

        public override void ReceiveDamage(int dmg)
        {
            Animator enemyAnimator = GetComponentInChildren<Animator>();
            _entityHealth.ChangeHealth(-dmg);
            _healthBar.transform.localScale = (new Vector3(
                _entityHealth.HealthPercentage(_entityHealth.Health),
                (float)0.1584, (float)0.09899999));
            enemyAnimator.SetBool("isHit", true);
            if (_entityHealth.Health <= 0)
            {
                enemyAnimator.SetBool("isDead", true);
                //Add animation so it isnt instant
                DefaultTile obstacleTile = WorldController.ObstacleLayer
                    .FirstOrDefault(t => t.GameObject == gameObject);
                WorldController.Instance.BaseLayer
                    .FirstOrDefault(t => t.XPos == obstacleTile.XPos && t.YPos == obstacleTile.YPos)
                    .Walkable = true;
                WorldController.ObstacleLayer.RemoveAt(WorldController.ObstacleLayer.IndexOf(obstacleTile));
                obstacleTile.GameObject = null;
                obstacleTile = null;
                if (deathEntityObject)
                {
                    GameObject newObject =
                        Instantiate(deathEntityObject, transform.position,
                            transform.rotation) as GameObject; // instatiate the object
                    newObject.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y,
                        transform.localScale.z);
                    Destroy(newObject, 5f);
                } 
                Destroy(gameObject);
                TurnController.Instance.gameOverEvent.Invoke();
            }
        }
    }
}