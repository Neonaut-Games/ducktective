using Character;
using UI.Uninteractable;
using UnityEngine;

namespace Items
{
    [RequireComponent(typeof(Rigidbody))]
    public class Coin : MonoBehaviour
    {
        
        public int value = 1;
        
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            
            AudioManager.Coin();
            CoinsManager.Deposit(value);
            
            Destroy(gameObject);
        }

    }
}