using JetBrains.Annotations;
using UnityEngine;

namespace Character.NPC.Temeghost
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
        private static readonly int IsMoving = Animator.StringToHash("isMoving");

        public void Update()
        {
            var playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
            var bossPosition = mortalRigidbody.position;


            if (Vector3.Distance(playerPosition, bossPosition) > sightRange)
                animator.SetBool(IsMoving, false);
            else
                animator.SetBool(IsMoving, true);
        }
        
        protected override void OnDeath()
        {
            if (loot == null) return;
            
            var lootPosition = transform.position + Vector3.up;
            for (int i = 0; i < Random.Range(lootMinimumAmount, lootMaximumAmount); i++) Instantiate(loot, lootPosition, Quaternion.identity);
            
            ambientParticles.Stop();
            ambientParticles.time = 2.0f;
        }
    }
}
