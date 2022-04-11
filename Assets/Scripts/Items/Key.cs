using UI.Inspect.Dialogue;
using UnityEngine;

namespace Items
{
    public class Key : DialogueTrigger
    {
        public void OnTriggerEnter(Collider other)
        {
            // If a player did not perform the event, ignore it.
            if (!other.CompareTag("Player")) return;
            
            // Start a new dialogue for the player
            shouldEnablePost = false;
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue, this);
            
            Destroy(gameObject);
        }
    }
}
