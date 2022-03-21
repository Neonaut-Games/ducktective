using System.Collections;
using Player;
using UnityEngine;

public class DamageableEntity : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private Collider _collider;

    [Header("Health Settings")]
    public int maxHealth = 100;
    private int _health;
    
    [Header("Animation Settings")]
    public Animator animator;
    public AudioSource takeDamageSound;
    public int fadeTime = 10;

    [Header("Loot Settings")]
    public GameObject loot;
    public int lootMinimum;
    public int lootMaximum;

    void Start()
    {
        _health = maxHealth;
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
    }
    
    public void TakeDamage(int damage)
    {
        /* The health of the entity will only decrement
        if the player has the appropriate quest level. This is to prevent
        players from already having 50 coins by the time they get to Meemaw. */
        if (PlayerLevel.GetLevel() >= 7) _health -= damage;
        
        // Play "take damage" audio cue
        takeDamageSound.Play();
        
        // If their health is 0 or below, transition to death function
        if (_health <= 0) {
            Die();
            return;
        }
        
        // Play hit animation
        animator.SetTrigger("wasAttacked");
    }

    private void Die()
    {
        Debug.Log("An enemy was killed.");
        
        // Animate death 
        animator.SetBool("isAlive", false);
        
        // Destroy the entity's Rigidbody and Collider
        Destroy(_rigidbody);
        Destroy(_collider);
        
        // Spawn loot
        for (int i = 0; i < Random.Range(lootMinimum, lootMaximum); i++)
        {
            Instantiate(loot, transform.position, Quaternion.identity);
        }

        StartCoroutine(FadeAway());
    }
    
    public IEnumerator FadeAway()
    {
        yield return new WaitForSeconds(fadeTime);
        Destroy(gameObject);
    }
}
