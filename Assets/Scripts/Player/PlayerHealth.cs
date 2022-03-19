using System.Collections;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class PlayerHealth : MonoBehaviour
    {
        
        public static int health = 100;

        [Header("Health Settings")]
        public int maxHealth = 100;
        
        [Header("UI Settings")]
        public Slider healthSlider;
        public GameObject gameOverUI;
        public TextMeshProUGUI secondsLeftUI;

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
            
            // Restrict player from moving around
            PlayerInspect.movementRestricted = true;
            
            // Start death animation
            FindObjectOfType<PlayerController>().playerAnimator.SetBool("isAlive", false);
            
            // Enable game over UI
            gameOverUI.SetActive(true);
            
            // Start timer for respawn
            StartCoroutine(RespawnTimer());
        }

        private IEnumerator RespawnTimer()
        {
            int secondsRemaining = 5;
            while (secondsRemaining > 0)
            {
                // Update UI to reflect seconds left
                secondsLeftUI.SetText("You will respawn in " + secondsRemaining + " seconds.");
                
                // Decrement seconds left
                secondsRemaining--;
                
                yield return new WaitForSeconds(1.0f);
            }
            Respawn();
        }

        public void Respawn()
        {
            Debug.Log("The player has respawned.");
            
            // Un-restrict player from moving around
            PlayerInspect.movementRestricted = false;
            
            // End death animation
            FindObjectOfType<PlayerController>().playerAnimator.SetBool("isAlive", true);
            
            // Enable game over UI
            gameOverUI.SetActive(false);
            
            // Teleport the player to the closest spawnpoint
            Transform closestPosition = GetClosestSpawnpoint();
            gameObject.transform.SetPositionAndRotation(closestPosition.position, gameObject.transform.rotation);
            
            // Set the players health back to the max
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

            if (closestPosition == null) Debug.LogError("No spawnpoints were found in the scene.");
            return closestPosition;
        }
    }
}
    