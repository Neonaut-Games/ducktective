using JetBrains.Annotations;
using UI.Inspect.Lock;
using UnityEngine;
using UnityEngine.Serialization;

namespace Character.NPC
{
    public class TemeghostBoss : Mortal
    {
        [Header("Cosmetic Settings")]
        public ParticleSystem ambientParticles;
        
        [Header("Combat Settings")]
        public int sightRange = 30;

        [Header("Loot Settings")]
        [CanBeNull] public GameObject loot;
        [FormerlySerializedAs("lootMaximumAmount")] public int lootAmount;
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
            for (int i = 0; i < lootAmount; i++) Instantiate(loot, transform.position, Quaternion.identity);
            ambientParticles.Stop();
            ambientParticles.time = 2.0f;
            LockTrigger.used = true;
        }
    }
}
