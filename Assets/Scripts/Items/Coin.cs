using UI.Uninteractable;
using UnityEngine;

namespace Items
{
    public class Coin : MonoBehaviour
    {
        
        public AudioSource pickupSound;

        public void OnTriggerEnter(Collider other)
        {
            // If a player did not perform the event, ignore it.
            if (!other.CompareTag("Player")) return;
            
            // Increment coins counter and UI element
            CoinsManager.Deposit(1);
            
            // Play the sound
            pickupSound.Play();
            
            // move the game object off screen while it finishes it's sound, then destroy it
            transform.position = Vector3.one * 9999f;
            Destroy(gameObject, pickupSound.clip.length);
        }
        
    }
}
