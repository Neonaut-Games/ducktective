using Character.Player;
using UI.Inspect.Dialogue;
using UnityEngine;

namespace Items
{
    public class Feather : DialogueTrigger
    {
        public void OnTriggerEnter(Collider other)
        {
            // If a player did not perform the event, ignore it.
            if (!other.CompareTag("Player")) return;
            
            // If the player does not have the required level, ignore it.
            if (shouldRequireQuestLevel) if (PlayerLevel.questLevel != requiredQuestLevel) return;
            
            // Start a new dialogue for the player
            shouldEnablePost = false;
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue, this);
            
            Destroy(gameObject);
        }
    }
}
