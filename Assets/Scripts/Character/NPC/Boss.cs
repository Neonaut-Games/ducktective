using System.Collections;
using Character.Player;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Serialization;
using Debug = System.Diagnostics.Debug;

namespace Character.NPC
{
    public class Boss : Mortal
    {
    
        [Header("Combat Settings")]
        public int sightRange = 30;
        
        [Header("Loot Settings")]
        [CanBeNull] public GameObject loot;
        public void Update()
        {
            var playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
            var bossPosition = mortalRigidbody.position;


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
            if (loot == null) return;
            
            Vector3 lootPos = transform.position;
            lootPos.y += 2;
            Instantiate(loot, lootPos, Quaternion.identity);
        }
    }
}
