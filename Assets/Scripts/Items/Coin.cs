using Dialogue;
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
            
            // Play the sound
            pickupSound.Play();
            
            amount++;
            Destroy(gameObject);
        }
        
    }
}
