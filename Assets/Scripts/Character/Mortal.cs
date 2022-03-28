using System.Collections;
using JetBrains.Annotations;
using Player;
using UnityEngine;

namespace Character
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(CapsuleCollider))]
    public abstract class Mortal : MonoBehaviour
    {

        [HideInInspector] public Rigidbody rigidbody;
        [HideInInspector] public CapsuleCollider collider;

        [Header("Health Settings")]
        public int minimumQuestLevelRequired;
        [HideInInspector] public int health;
        public int maxHealth = 100;
        
        [Header("Animation Settings")]
        public Animator animator;
        public AudioSource takeDamageSound;
        public int bodyDecayTime = 10;

        private void Start()
        {
            /* Initialize the entity's health by
            setting it to it's maximum value. */
            health = maxHealth;
            
            /* Initialize all required components for
            mortal entities into their respective variables. */
            rigidbody = GetComponent<Rigidbody>();
            collider = GetComponent<CapsuleCollider>();
        }

        public void TakeDamage(int amount)
        {
            /* The health of the entity will only decrement if the player has the appropriate quest level.
            This is to stop players from completing multiple quests at once and/or cheesing the game. */
            if (PlayerLevel.GetLevel() >= minimumQuestLevelRequired)
            {
                health -= amount;
                takeDamageSound.Play();
            }

            /* If the mortal's health is == 0 or
            below, transition to the death function. */
            if (health <= 0) {
                Die();
                return;
            }

            // Play hit animation
            animator.SetTrigger("takeHit");
        }
        
        public void Die()
        {
            Debug.Log(gameObject.name + " was killed.");
            
            // Animate death 
            animator.SetTrigger("die");
        
            /* Disable the mortal's standing collider
            and make the Rigidbody kinematic to better sell the
            death animation physics. */
            rigidbody.isKinematic = true;
            collider.enabled = false;

            StartCoroutine(BodyDecay());
            
            DeathReward();
        }


        public abstract void DeathReward();

        private IEnumerator BodyDecay()
        {
            yield return new WaitForSeconds(bodyDecayTime);
            Destroy(gameObject);
        }
        
    }

}