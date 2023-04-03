using System;
using System.Collections.Generic;
using UnityEngine;

namespace ReDesign.Entities
{
    public class Player : Entity
    {
        private UnitHealth _playerHealth;
        private int XPos = 0;
        private int YPos = 0;
        private GameObject _gameObject;
        [SerializeField] private GameObject _healthBar;

        private List<Attack> _attacks = new List<Attack>
        {
            new Fireball(),
            new Frostbolt()
        };

        private void Awake()
        {
            _playerHealth = new UnitHealth(20, 20);
        }

        public override void NextAction()
        {
            //do sum stuff
            TurnController.ResolveTurn();
        }

        public override void Move()
        {
            throw new System.NotImplementedException();
        }

        public override void Attack()
        {
            throw new System.NotImplementedException();
        }
    }
}