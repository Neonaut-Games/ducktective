using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{

    public static int health = 100;
    public int maxHealth = 100;
    public Slider healthSlider;

    private void Start()
    {
        RefreshHealth();
    }

    public void RefreshHealth()
    {
        // Adjust slider health stat (decoration)
        healthSlider.maxValue = maxHealth;
        healthSlider.value = health;
    }
    
    public void SetHealth(int amount)
    {
        // Adjust global health stat (real)
        health = AdjustHealth(amount);
        
        // Adjust slider health stat (decoration)
        RefreshHealth();
        
        // Check death conditions
        if (health <= 0) Die();
    }

    private int AdjustHealth(int amount)
    {
        if (amount > maxHealth) amount = maxHealth;
        if (amount < 0) amount = 0;
        return amount;
    }

    public void TakeDamage(int amount)
    {
        Debug.Log("The player took damage (" + amount + "hp).");
        
        SetHealth(health - amount);
    }

    public void Die()
    {
        Debug.Log("The player has died.");
        
        Transform closestPosition = GetClosestSpawnpoint();
        gameObject.transform.SetPositionAndRotation(closestPosition.position, gameObject.transform.rotation);
        SetHealth(maxHealth);
    }

    private Transform GetClosestSpawnpoint()
    {
        Transform closestPosition = null;
        float minimumDistance = Mathf.Infinity;
        foreach (GameObject spawnpoint in GameObject.FindGameObjectsWithTag("Respawn"))
        {
            float thisDistance = Vector3.Distance(spawnpoint.transform.position, gameObject.transform.position);
            if (thisDistance < minimumDistance)
            {
                closestPosition = spawnpoint.transform;
                minimumDistance = thisDistance;
            }
        }

        return closestPosition;
    }
}
    