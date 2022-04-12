using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Character.Player
{
    public class PlayerHealth : MonoBehaviour
    {
        private PlayerController _player;
        public static int health = 100;

        [Header("Health Settings")]
        public int maxHealth = 100;
        public Slider healthSlider;
        public Animator damageVignette;

        [Header("Death Settings")]
        public GameObject gameOverUI;
        public TextMeshProUGUI secondsLeftUI;
        private static readonly int IsAlive = Animator.StringToHash("isAlive");
        private static readonly int Hurt = Animator.StringToHash("hurt");

        private void Start()
        {
            Refresh();
            _player = FindObjectOfType<PlayerController>();
        }

        private void Refresh()
        {
            // Adjust slider health stat (decoration)
            healthSlider.maxValue = maxHealth;
            healthSlider.value = health;
        }

        private void SetHealth(int amount)
        {
            // Adjust global health stat (real)
            health = AdjustHealth(amount);

            // Adjust slider health stat (decoration)
            Refresh();

            // Check death conditions
            if (health <= 0) Die();
        }

        private int AdjustHealth(int amount)
        {
            if (amount > maxHealth) amount = maxHealth;
            else if (amount < 0) amount = 0;
            return amount;
        }

        public void TakeDamage(int amount)
        {
            // Play "take damage" audio cue
            AudioManager.Hurt();
            
            DuckLog.Normal("The player took damage (" + amount + "hp).");
            damageVignette.SetTrigger(Hurt);
            SetHealth(health - amount);
        }
        
        public void Heal(int amount)
        {
            DuckLog.Normal("The player was healed (" + amount + "hp).");
            SetHealth(health + amount);
        }

        private void Die()
        {
            PlayerStats.totalDeaths++;
            DuckLog.Normal("The player has died.");

            // Restrict player from moving around
            _player.characterController.enabled = false;
            PlayerInspect.movementRestricted = true;

            // Start death animation
            _player.playerAnimator.SetBool(IsAlive, false);
            
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

        private void Respawn()
        {
            DuckLog.Normal("The player has respawned.");

            // End death animation
            _player.playerAnimator.SetBool(IsAlive, true);
            
            // Disable game over UI
            gameOverUI.SetActive(false);
            
            // Teleport the player to the closest spawnpoint
            Transform closestPosition = GetClosestSpawnpoint();
            gameObject.transform.SetPositionAndRotation(closestPosition.position, gameObject.transform.rotation);

            // Un-restrict player from moving around
            _player.characterController.enabled = true;
            PlayerInspect.movementRestricted = false;
            
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
                if (thisDistance < minimumDistance && thisDistance > 1.0)
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
    