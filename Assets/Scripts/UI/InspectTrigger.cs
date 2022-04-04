using Character.Player;
using UnityEngine;

namespace UI
{
    public abstract class InspectTrigger : MonoBehaviour
    {
        
        private Animator _inspectIndicator;
        private AudioSource _inspectSound;
        
        [Header("Trigger Requirements")]
        public bool shouldRequireQuestLevel;
        public int requiredQuestLevel;
        
        public void Start()
        {
            _inspectIndicator = GameObject.FindGameObjectWithTag("InspectIndicator").GetComponent<Animator>();
            _inspectSound = _inspectIndicator.GetComponent<AudioSource>();
        }

        public abstract void Trigger();

        private void OnTriggerEnter(Collider other)
        {
            // If the object is not a player, ignore the event.
            if (!other.CompareTag("Player")) return;

            // If the player does not have the required quest level, ignore the event.
            if (shouldRequireQuestLevel)
                if (PlayerLevel.questLevel != requiredQuestLevel)
                    return;

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