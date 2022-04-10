using JetBrains.Annotations;
using UnityEngine;

namespace Character.NPC
{
    public class Temeghost : Mortal
    {
        [Header("Cosmetic Settings")]
        public ParticleSystem ambientParticles;
        
        [Header("Combat Settings")]
        public int sightRange = 30;

        [Header("Loot Settings")]
        [CanBeNull] public GameObject loot;
        public int lootMinimumAmount;
        public int lootMaximumAmount;

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

        public override void OnTakeDamage()
        {
        }

        public override void OnDeath()
        {
            if (loot == null) return;
            for (int i = 0; i < Random.Range(lootMinimumAmount, lootMaximumAmount); i++)
            {
                Instantiate(loot, transform.position, Quaternion.identity);
            }
            ambientParticles.Stop();
            ambientParticles.time = 2.0f;
        }
    }
}
