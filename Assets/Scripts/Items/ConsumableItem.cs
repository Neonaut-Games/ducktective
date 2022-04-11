using UnityEngine;

namespace Items
{
    [RequireComponent(typeof(Rigidbody))]
    public abstract class ConsumableItem : MonoBehaviour
    {
        
        private Rigidbody _rigidbody;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            
            Consume();
            Destroy(gameObject);
        }

        public abstract void Consume();
        private void OnCollisionStay(Collision collisionInfo)
        {
            // If a player did not perform the event, ignore it.
            if (!collisionInfo.gameObject.CompareTag("Player")) return;
            
            // Get the Rigidbody of the coin if it does not already have it
            if (_rigidbody == null) _rigidbody = GetComponent<Rigidbody>();
            
            _rigidbody.AddForce(new Vector3(0.0f, 1.0f, 0.0f));
        }
    }
}