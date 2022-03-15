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
        healthSlider.maxValue = maxHealth;
        healthSlider.value = health;
    }

    private int AdjustHealth(int amount)
    {
        if (amount > maxHealth) amount = maxHealth;
        if (amount < 0) amount = 0;
        return amount;
    }

    public void TakeDamage(int amount)
    {
        SetHealth(health - amount);
    }
}
