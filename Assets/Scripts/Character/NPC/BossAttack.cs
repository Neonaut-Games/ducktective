using Character.Player;
using UnityEngine;

namespace Character.NPC
{
    public class BossAttack : MonoBehaviour
    {

        [Header("Required Settings")]
        public Transform attackPoint;
        public float attackRange = 1.0f;
        public LayerMask damageLayer;
        
        [Header("Cosmetic Settings")]
        public AudioSource attackSound;
        
        public void Attack()
        {
            // Deal damage at the transform point
            Collider[] entities = Physics.OverlapSphere(attackPoint.position, attackRange, damageLayer.value);
            if (entities.Length > 0) PerformSuccessfulAttack(entities);
            else PerformFailedAttack();
        }

        private void PerformSuccessfulAttack(Collider[] entities)
        {
            attackSound.Play();
            
            /* Have each enemy that was touched by the boss's attack collider
            take damage. This includes the player and damageable NPCs. */
            foreach (var entity in entities)
            {
                // If the damaged entity is a player
                if (entity.CompareTag("Player"))
                {
                    Debug.Log(gameObject.name + " damaged the Player.");
                    entity.GetComponent<PlayerHealth>().TakeDamage(20);
                }
            }
        }

        private void PerformFailedAttack() { }
        
    }
}
