using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ReDesign.Entities{
    public abstract class Enemy : Entity
    {
        [SerializeField] private GameObject _healthBar;
        public override void ReceiveDamage(int dmg)
        {
            EntityAnimator enemyAnimator = GetComponentInChildren<EntityAnimator>();
            _entityHealth.ChangeHealth(-dmg);
            _healthBar.transform.localScale = (new Vector3(
                _entityHealth.HealthPercentage(_entityHealth.Health),
                (float)0.1584, (float)0.09899999));
            enemyAnimator.SetHit();
            if (_entityHealth.Health <= 0)
            {
                enemyAnimator.SetDead();
                //Add animation so it isnt instant
                DefaultTile obstacleTile = WorldController.ObstacleLayer
                    .FirstOrDefault(t => t.GameObject == gameObject);
                WorldController.Instance.BaseLayer.FirstOrDefault(t => t.XPos == obstacleTile.XPos && t.YPos == obstacleTile.YPos)
                    .Walkable = true;
                WorldController.ObstacleLayer.RemoveAt(WorldController.ObstacleLayer.IndexOf(obstacleTile));
                obstacleTile.GameObject = null;
                obstacleTile = null;

                Destroy(this.gameObject);

                TurnController.Instance.gameOverEvent.Invoke();
            }
        }
    }
}
