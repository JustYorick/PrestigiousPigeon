using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ReDesign.Entities{
    public abstract class Enemy : Entity
    {
        [SerializeField] private GameObject _healthBar;
        [SerializeField] private float deathAnimationTime = 1f;
        public override void ReceiveDamage(int dmg)
        {
            _entityHealth.ChangeHealth(-dmg);
            _healthBar.transform.localScale = (new Vector3(
                _entityHealth.HealthPercentage(_entityHealth.Health),
                (float)0.1584, (float)0.09899999));
            
            if (_entityHealth.Health <= 0)
            {
                //Add animation so it isnt instant
                DefaultTile obstacleTile = WorldController.ObstacleLayer
                    .FirstOrDefault(t => t.GameObject == gameObject);
                WorldController.Instance.BaseLayer.FirstOrDefault(t => t.XPos == obstacleTile.XPos && t.YPos == obstacleTile.YPos)
                    .Walkable = true;
                WorldController.ObstacleLayer.RemoveAt(WorldController.ObstacleLayer.IndexOf(obstacleTile));
                obstacleTile.GameObject = null;
                obstacleTile = null;

                Animator animator = GetComponentInChildren<Animator>();
                animator.SetBool("isDead", true);
                Destroy(this.gameObject, deathAnimationTime);

                TurnController.Instance.gameOverEvent.Invoke();
            }
        }
    }
}
