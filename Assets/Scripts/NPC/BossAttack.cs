using Player;
using UnityEngine;

namespace NPC
{
    public class BossAttack : MonoBehaviour
    {

        public Transform attackPoint;
        public float attackRange;
        public LayerMask damageLayer;
        public AudioSource failedAttackSound;
        
        public void Attack()
        {
            // Deal damage at the transform point
            Collider[] results = Physics.OverlapSphere(attackPoint.position, attackRange, damageLayer.value);
            if (results.Length > 0) DamageEntities(results);
            else failedAttackSound.Play(); // Play attack miss sound
        }

        private void DamageEntities(Collider[] entities)
        {
            // Have each entity take damage
            foreach (Collider entity in entities)
            {
                Debug.Log("Boss damaged player.");
                if (entity.CompareTag("Player")) entity.GetComponent<PlayerHealth>().TakeDamage(20);
            }
        }
        
    }
}
