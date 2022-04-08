using System.Collections;
using Character.Player;
using UnityEngine;

namespace Character
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(CapsuleCollider))]
    public abstract class Mortal : MonoBehaviour
    {

        [HideInInspector] public Rigidbody mortalRigidbody;
        [HideInInspector] public CapsuleCollider mortalCollider;

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
            mortalRigidbody = GetComponent<Rigidbody>();
            mortalCollider = GetComponent<CapsuleCollider>();
        }

        /* This function manages all damage taken by Mortal enemies
        (unless expanded upon or changed by a subclass) and returns a
        boolean value indicating whether or not the damage killed the enemy. */
        public bool TakeDamage(int amount)
        {
            /* The health of the entity will only decrement if the player has the appropriate quest level.
            This is to stop players from completing multiple quests at once and/or cheesing the game. */
            if (PlayerLevel.questLevel >= minimumQuestLevelRequired)
            {
                health -= amount;
            }
            
            takeDamageSound.Play();

            /* If the mortal's health is == 0 or
            below, transition to the death function. */
            if (health <= 0) {
                Die();
                return true;
            }

            // Play hit animation
            animator.SetTrigger("takeHit");
            return false;
        }

        private void Die()
        {
            DuckLog.Normal(gameObject.name + " was killed.");
            
            // Animate death 
            animator.SetTrigger("die");
        
            /* Disable the mortal's standing collider
            and make the Rigidbody kinematic to better sell the
            death animation physics. */
            mortalRigidbody.isKinematic = true;
            mortalCollider.enabled = false;

            StartCoroutine(BodyDescend());
            StartCoroutine(BodyDecay());
            
            DeathReward();
        }
        
        public abstract void DeathReward();

        public IEnumerator BodyDescend()
        {
            var destination = mortalRigidbody.position;
            destination.y -= 10;
            while (true)
            {
                mortalRigidbody.MovePosition(destination);
            }
        }

        private IEnumerator BodyDecay()
        {
            yield return new WaitForSeconds(bodyDecayTime);
            Destroy(gameObject);
            StopAllCoroutines();
        }
        
    }

}