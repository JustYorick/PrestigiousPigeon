using System.Collections.Generic;
using UnityEngine;

namespace ReDesign
{
    public abstract class Entity
    {
        private int Health { get; set; }
        private int MaxHealth { get; set; }
        private int XPos { get; set; }
        private int YPos { get; set; }
        private List<Attack> Attacks { get; set; }

        public abstract void NextAction();
        public abstract void Move();
        public abstract void Attack();
    }

    public class Player : Entity
    {
        private UnitHealth _playerHealth = new UnitHealth(20, 20);
        private int XPos = 0;
        private int YPos = 0;
        private GameObject gameObject;

        private List<Attack> _attacks = new List<Attack>
        {
            new Fireball(),
            new Frostbolt()
        };

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

    public class Slime : Entity
    {
        private UnitHealth _slimeHealth = new UnitHealth(5, 5);
        private int XPos = 0;
        private int YPos = 0;
        private GameObject _gameObject;

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
            _slimeHealth.DmgUnit(5);
        }
        
    }
}