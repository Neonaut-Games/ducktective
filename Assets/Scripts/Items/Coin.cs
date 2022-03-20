using Player;
using UnityEngine;

namespace Items
{
    public class Coin : MonoBehaviour
    {
        
        public static int amount;
        public AudioSource pickupSound;

        public void OnTriggerEnter(Collider other)
        {
            // If a player did not perform the event, ignore it.
            if (!other.CompareTag("Player")) return;
            
            // Increment coins counter and UI element
            amount++;
            FindObjectOfType<CoinsManager>().RefreshAmount();
            
            // Level up the player's quest level if applicable
            if (amount >= 50) PlayerLevel.SetLevel(8);

                // Play the sound
            pickupSound.Play();
            
            // move the game object off screen while it finishes it's sound, then destroy it
            transform.position = Vector3.one * 9999f;
            Destroy(gameObject, pickupSound.clip.length);
        }
        
    }
}
