using System.Collections;
using UnityEngine;

public class DamageableEntity : MonoBehaviour
{

    [Header("Health Settings")]
    public int maxHealth = 100;
    private int _health;
    
    [Header("Animation Settings")]
    public Animator animator;
    public int fadeTime = 10;

    [Header("Loot Settings")]
    public GameObject loot;
    public int lootMinimum;
    public int lootMaximum;

    void Start() => _health = maxHealth;
    
    public void TakeDamage(int damage)
    {
        _health -= damage;
        if (_health < 0) Die();
        else animator.SetBool("beingHit", true);
    }
    

    private void Die()
    {
        Debug.Log("An enemy was killed.");
        
        // Animate death 
        animator.SetBool("isAlive", false);
        
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
