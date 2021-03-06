using System;
using Character.Player;
using UI.Uninteractable;
using UnityEngine;
using UnityEngine.Serialization;

namespace SceneManagement
{
    public class PlayerThrow : MonoBehaviour
    {
        private static bool _beingThrown;
        private static Vector3 _destination;
    
        [Header("Scene Settings")]
        public String sceneName;
        public float destinationX, destinationY, destinationZ;

        [Header("Player Settings")]
        public bool shouldRequireQuestLevel;
        [FormerlySerializedAs("requiredQuestLevel")] public int minQuestLevel;
        public int maxQuestLevel = 10;

        private void Awake()
        {
            // If the player is not being thrown, ignore the event.
            if (!_beingThrown) return;
        
            // Move the player to the destination
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.transform.SetPositionAndRotation(_destination, player.transform.rotation);
        
            // Re-enable the character controller
            player.GetComponent<CharacterController>().enabled = true;

            // End the thrown state
            _beingThrown = false;
        }
    
        private void OnTriggerEnter(Collider other) => Throw(other);
        private void OnTriggerStay(Collider other) => Throw(other);
        
        private void Throw(Collider other)
        {
            if (_beingThrown) return;
            
            // If the object entering the trigger is not a player, ignore the event.
            if (!other.CompareTag("Player")) return;

            // If the player does not have the required quest level, ignore the event.
            if (shouldRequireQuestLevel)
            {
                if (!(PlayerLevel.questLevel >= minQuestLevel && PlayerLevel.questLevel <= maxQuestLevel)) return;
            }
            
            // Disable the character controller (temporarily)
            var player = FindObjectOfType<PlayerController>();
            player.characterController.enabled = false;
        
            // Prepare destination position
            _beingThrown = true;
            _destination = new Vector3(destinationX, destinationY, destinationZ);
        
            // Load the given scene
            FindObjectOfType<LoadingScreen>().Load(sceneName);
        }

    }
}
