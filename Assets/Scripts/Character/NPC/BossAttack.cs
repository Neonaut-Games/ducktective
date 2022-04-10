using Character.Player;
using JetBrains.Annotations;
using UnityEngine;

namespace Character.NPC
{
    public class BossAttack : MonoBehaviour
    {

        [Header("Required Settings")]
        public Transform attackPoint;
        public float attackRange = 1.0f;
        public LayerMask damageLayer;
        public int attackDamage = 20;
        
        [Header("Cosmetic Settings")]
        [CanBeNull] public ParticleSystem attackParticles;
        
        public void Attack()
        {
            DuckLog.Normal("The boss attempted an attack.");
            
            // Deal damage at the transform point
            Collider[] entities = Physics.OverlapSphere(attackPoint.position, attackRange, damageLayer.value);
            if (entities.Length > 0) PerformSuccessfulAttack(entities);
            else PerformFailedAttack();
            
            AudioManager.Impact();
            if (attackParticles != null) attackParticles.Play();
        }

        private void PerformSuccessfulAttack(Collider[] entities)
        {
            /* Have each enemy that was touched by the boss's attack collider
            take damage. This includes the player and damageable NPCs. */
            foreach (var entity in entities)
            {
                // If the damaged entity is a player
                if (entity.CompareTag("Player"))
                {
                    DuckLog.Normal(gameObject.name + " damaged the Player.");
                    entity.GetComponent<PlayerHealth>().TakeDamage(attackDamage);
                }
            }
        }

        private void PerformFailedAttack() { }

        public void HurtBoss() => AudioManager.HurtBoss();
        public void AttackBoss() => AudioManager.AttackBoss();

    }
}
