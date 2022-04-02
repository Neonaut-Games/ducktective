using Character.Player;
using Player;
using UI.Dialogue;
using UnityEngine;

namespace Items
{
    public class BlackFeather : MonoBehaviour
    {
        
        public static int amount;
        
        [Header("Dialogue")]
        public Dialogue pickupMessage;
        
        [Header("Trigger Requirements")]
        public bool shouldRequireQuestLevel;
        public int requiredQuestLevel;

        [Header("Completion Rewards")]
        public bool shouldChangeQuestLevel;
        public int rewardedQuestLevel;
        
        public void OnTriggerEnter(Collider other)
        {
            // If a player did not perform the event, ignore it.
            if (!other.CompareTag("Player")) return;
            
            // If the player does not have the required level, ignore it.
            if (shouldRequireQuestLevel) if (PlayerLevel.GetLevel() != requiredQuestLevel) return;
            
            // Start a new dialogue for the player
            FindObjectOfType<DialogueManager>().StartDialogue(pickupMessage);
            
            amount++;
            Destroy(gameObject);
            
            // Reward the player if applicable
            if (shouldChangeQuestLevel) PlayerLevel.SetLevel(rewardedQuestLevel);
        }
        
    }
}
