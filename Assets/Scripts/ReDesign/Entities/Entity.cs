using System.Collections.Generic;
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
            _entityHealth.DmgUnit(dmg);
            _healthBar.transform.localScale = (new Vector3(
                _entityHealth.HealthPercentage(_entityHealth.Health),
                (float)0.1584, (float)0.09899999));

            if (_entityHealth.Health <= 0)
            {
                //Add animation so it isnt instant
                Destroy(this.gameObject);
            }
        }
    }
}