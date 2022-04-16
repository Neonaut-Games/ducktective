using JetBrains.Annotations;
using UnityEngine;

namespace Character.NPC.Temeghost
{
    public class Temeghost : Hostile
    {
        [Header("Cosmetic Settings")]
        public ParticleSystem ambientParticles;
        
        [Header("Loot Settings")]
        [CanBeNull] public GameObject loot;
        public int lootMinimumAmount;
        public int lootMaximumAmount;
        
        protected override void AfterStart() {}
        
        protected override void OnAggro() {}
        
        protected override void OnDeAggro() {}
        
        protected override void OnTakeDamage() {} 

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
