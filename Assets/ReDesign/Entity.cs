using System.Collections.Generic;
using UnityEngine;

namespace ReDesign
{
    public abstract class Entity
    {
        private int Health { get; set; }
        private int XPos { get; set; }
        private int YPos { get; set; }
        private List<Attack> Attacks { get; set; }

        public abstract void NextAction();
        public abstract void Move();
        public abstract void Attack();
    }

    public class Player : Entity
    {
        private int Health = 100;
        private int XPos = 0;
        private int YPos = 0;
        private GameObject gameObject;
        private List<Attack> Attacks = new List<Attack>
        {
            new Frostbolt()
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
            throw new System.NotImplementedException();
        }
    }
}