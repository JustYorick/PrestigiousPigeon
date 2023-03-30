using System.Collections.Generic;
using UnityEngine;

namespace ReDesign.Entities
{
    public class Slime : Entity
    {
        private UnitHealth _slimeHealth = new UnitHealth(5, 5);
        private int XPos = 0;
        private int YPos = 0;
        private GameObject _gameObject;
        [SerializeField] private GameObject _healthBar;

        private List<Attack> _attacks = new List<Attack>()
        {
            new SlimeAttack()
        };

        public override void NextAction()
        {
            throw new System.NotImplementedException();
        }

        public override void Move()
        {
            throw new System.NotImplementedException();
        }

        public override void Attack()
        {
        }
        
        private void Update()
        {
            if (Input.GetKeyDown("f"))
            {
                _slimeHealth.DmgUnit(1);
                _healthBar.transform.localScale = (new Vector3(
                    _slimeHealth.HealthPercentage(_slimeHealth.Health),
                    (float)0.1584, (float)0.09899999));
            }
        }
    }
}