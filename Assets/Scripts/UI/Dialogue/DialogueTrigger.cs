using Character.Player;
using JetBrains.Annotations;
using Player;
using UnityEngine;

namespace UI.Dialogue
{
    public class DialogueTrigger : MonoBehaviour
    {
        private Animator _inspectIndicator;
        private AudioSource _inspectSound;
    
        [Header("Dialogue Settings")]
        public Dialogue dialogue;

        [Header("Trigger Requirements")]
        public bool shouldRequireQuestLevel;
        public int requiredQuestLevel;
    
        [Header("Completion Rewards")]
        public bool shouldChangeQuestLevel;
        public int rewardedQuestLevel;
        [CanBeNull] public GameObject rewardObject;

        public void Start()
        {
            _inspectIndicator = GameObject.FindGameObjectWithTag("InspectIndicator").GetComponent<Animator>();
            _inspectSound = _inspectIndicator.GetComponent<AudioSource>();
        }

        public void TriggerDialogue()
        {
            Debug.Log("Dialogue was triggered by " + gameObject.name);
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        }

        private void OnTriggerEnter(Collider other)
        {
            // If the object is not a player, ignore the event.
            if (!other.CompareTag("Player")) return;
        
            // If the player does not have the required quest level, ignore the event.
            if (shouldRequireQuestLevel) if (PlayerLevel.GetLevel() != requiredQuestLevel) return;
        
            _inspectIndicator.SetBool("isEnabled", true);
            PlayerInspect.canInspect = true;
            PlayerInspect.loadedTrigger = this;
        
            // Play audio cue
            _inspectSound.Play();
        }

        private void OnTriggerExit(Collider other)
        {
            // If the object is not a player, ignore the event.
            if (!other.CompareTag("Player")) return;
        
            // If the player does not have the required quest level, ignore the event.
            if (shouldRequireQuestLevel) if (PlayerLevel.IsQualified(requiredQuestLevel)) return;

            _inspectIndicator.SetBool("isEnabled", false);
            PlayerInspect.canInspect = false;
            PlayerInspect.loadedTrigger = null;
        }

    }
}
