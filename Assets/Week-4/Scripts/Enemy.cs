using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ClassDemo
{
    public class Enemy : MonoBehaviour
    {

        public int health = 10;

        [SerializeField] private Player target;


        /// <summary>
        /// Damages the enemy until they are dead
        /// </summary>
        /// <param name="amt">How much damage to deal the enemy</param>
        public void Damage(int amt)
        {
            health -= amt;
        }

        [ContextMenu("Attack")]
        void Attack()
        {
            target.Damage(3);
        }

    }
}