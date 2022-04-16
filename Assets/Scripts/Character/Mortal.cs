using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(CapsuleCollider))]
    public abstract class Mortal : MonoBehaviour
    {

        [HideInInspector] public bool isAlive = true;
        [HideInInspector] public SkinnedMeshRenderer[] renderers;
        [HideInInspector] public Rigidbody mortalRigidbody;
        [HideInInspector] public CapsuleCollider mortalCollider;

        [Header("Health Settings")]
        [HideInInspector] public int health;
        public int maxHealth = 100;
        
        [Header("Animation Settings")]
        public Animator animator;
        public AudioSource takeDamageSound;
        public int bodyDecayTime = 10;

        private static readonly int Die1 = Animator.StringToHash("die");
        private static readonly int TakeHit = Animator.StringToHash("takeHit");

        private void Start()
        {
            /* Initialize the entity's health by
            setting it to it's maximum value. */
            health = maxHealth;
            
            /* Initialize all required components for
            mortal entities into their respective variables. */
            mortalRigidbody = GetComponent<Rigidbody>();
            mortalCollider = GetComponent<CapsuleCollider>();
            renderers = GetComponentsInChildren<SkinnedMeshRenderer>();
            
            AfterStart();
        }

        protected abstract void AfterStart();

        /* This function manages all damage taken by Mortal enemies
        (unless expanded upon or changed by a subclass) and returns a
        boolean value indicating whether or not the damage killed the enemy. */
        public bool TakeDamage(int amount)
        { 
            health -= amount;
            
            takeDamageSound.Play();
            StartCoroutine(DamageTint());
            OnTakeDamage();

            /* If the mortal's health is == 0 or
            below, transition to the death function. */
            if (health <= 0) {
                Die();
                return true;
            }
            
            animator.SetTrigger(TakeHit);
            return false;
        }
        
        protected abstract void OnTakeDamage();

        private IEnumerator DamageTint()
        {
            yield return new WaitForSeconds(0.1f);
            
            var colors = new Dictionary<string, Color>();
            foreach (var component in renderers)
            {
                var previousColor = component.material.color;
                colors.Add(component.name, previousColor);
                previousColor.r = 1;
                previousColor.g /= 2;
                previousColor.b /= 2;
                component.material.color = previousColor;
            }
            
            yield return new WaitForSeconds(0.5f);
            
            foreach (var component in renderers)
            {
                colors.TryGetValue(component.name, out var previousMaterial);
                component.material.color = previousMaterial;
            }
        }

        private void Die()
        {
            DuckLog.Normal(gameObject.name + " was killed.");

            isAlive = false;
            
            // Animate death 
            animator.SetTrigger(Die1);
        
            /* Disable the mortal's standing collider
            and make the Rigidbody kinematic to better sell the
            death animation physics. */
            mortalRigidbody.isKinematic = true;
            mortalCollider.enabled = false;
            
            OnDeath();
            StartCoroutine(BodyDecay());
        }

        protected abstract void OnDeath();

        private IEnumerator BodyDecay()
        {
            yield return new WaitForSeconds(bodyDecayTime);
            Destroy(gameObject);
            StopAllCoroutines();
        }

    }

}