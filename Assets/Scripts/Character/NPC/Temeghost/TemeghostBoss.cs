using JetBrains.Annotations;
using UI.Inspect.Lock;
using UnityEngine;
using UnityEngine.Serialization;

namespace Character.NPC.Temeghost
{
    public class TemeghostBoss : Boss
    {
        [Header("Cosmetic Settings")]
        public ParticleSystem ambientParticles;

        [Header("Loot Settings")]
        [CanBeNull] public GameObject loot;
        [FormerlySerializedAs("lootMaximumAmount")] public int lootAmount;
        
        private static readonly int Enabled = Animator.StringToHash("isEnabled");

        protected override void OnDeath()
        {
            healthBarAnimator.SetBool(Enabled, false);
            
            if (loot == null) return;
            for (int i = 0; i < lootAmount; i++) Instantiate(loot, transform.position, Quaternion.identity);
            ambientParticles.Stop();
            ambientParticles.time = 2.0f;
            LockTrigger.used = true;
        }
    }
}
