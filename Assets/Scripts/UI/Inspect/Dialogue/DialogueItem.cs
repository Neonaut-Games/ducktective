using Character.Player;
using UnityEngine;

namespace UI.Inspect.Dialogue
{
    public class DialogueItem : DialogueTrigger
    {
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            LoadInspectTrigger(this);
            Trigger();
        }

        public override void Trigger()
        {
            DuckLog.Normal("Dialogue was triggered by item " + gameObject.name);

            // If the player does not have the required level, ignore it.
            if (shouldRequireQuestLevel) if (PlayerLevel.questLevel != requiredQuestLevel) return;
            
            // Start a new dialogue for the player
            shouldEnablePost = false;
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue, this);
            
            Destroy(gameObject);
        }

    }
}
