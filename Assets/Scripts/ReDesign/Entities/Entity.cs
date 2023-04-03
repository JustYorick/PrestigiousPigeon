using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ReDesign.Entities
{
    public abstract class Entity : MonoBehaviour
    {
        public UnitHealth _entityHealth { get; set; }
        [SerializeField] private GameObject _healthBar;
        //public int MaxHealth { get; set; }
        private List<AttacksAndSpells> Attacks { get; set; }

        public abstract void NextAction();
        public abstract void Move();
        public abstract void Attack();
        public void ReceiveDamage(int dmg)
        {
            _entityHealth.ChangeHealth(-dmg);
            _healthBar.transform.localScale = (new Vector3(
                _entityHealth.HealthPercentage(_entityHealth.Health),
                (float)0.1584, (float)0.09899999));

            if (_entityHealth.Health <= 0)
            {
                //Add animation so it isnt instant
                DefaultTile obstacleTile = WorldController.Instance.ObstacleLayer.Where(t => t.GameObject == gameObject).FirstOrDefault();
                obstacleTile.GameObject = null;
                WorldController.Instance.BaseLayer.Where(t => t.XPos == obstacleTile.XPos && t.YPos == obstacleTile.YPos).FirstOrDefault().Walkable = true;
                Destroy(this.gameObject);
            }
        }
    }
}