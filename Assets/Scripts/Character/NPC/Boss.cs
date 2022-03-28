using System;
using UnityEngine;

namespace Character.NPC
{
    public class Boss : Mortal
    {
        public int sightRange = 30;
        public void Update()
        {
            var playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
            var bossPosition = rigidbody.position;

            if (Vector3.Distance(playerPosition, bossPosition) > sightRange)
            {
                animator.SetBool("isMoving", false);
            }
            else
            {
                animator.SetBool("isMoving", true);
            }
        }

        public override void DeathReward()
        {
            Debug.Log("The boss death reward function has run.");
        }
    }
}
