using System.Collections.Generic;
using UnityEngine;

namespace ReDesign.Entities
{
    public abstract class Entity : MonoBehaviour
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
}