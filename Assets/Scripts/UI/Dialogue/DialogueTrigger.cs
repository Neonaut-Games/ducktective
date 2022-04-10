using JetBrains.Annotations;
using UnityEngine;

namespace UI.Dialogue
{
    public class DialogueTrigger : InspectTrigger
    {
        
        [Header("Dialogue Settings")]
        public Dialogue dialogue;
        public bool shouldEnablePost = true;

        [Header("Completion Rewards")]
        public bool shouldChangeQuestLevel;
        public int rewardedQuestLevel;
        [CanBeNull] public GameObject rewardObject;
        

        public override void Trigger()
        {
            DuckLog.Normal("Dialogue was triggered by " + gameObject.name);
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue, this);
        }

    }
}
