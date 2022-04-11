using Character.Player;
using UnityEngine;

namespace UI.Inspect
{
    public abstract class InspectTrigger : MonoBehaviour
    {
        
        private Animator _inspectIndicator;
        private AudioSource _inspectSound;
        
        [Header("Trigger Requirements")]
        public bool shouldRequireQuestLevel;
        public int requiredQuestLevel;
        private static readonly int IsEnabled = Animator.StringToHash("isEnabled");

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

            _inspectIndicator.SetBool(IsEnabled, true);
            LoadInspectTrigger(this);

            // Play audio cue
            _inspectSound.Play();
        }

        private void OnTriggerExit(Collider other)
        {
            // If the object is not a player, ignore the event.
            if (!other.CompareTag("Player")) return;
        
            // If the player does not have the required quest level, ignore the event.
            //if (shouldRequireQuestLevel) if (PlayerLevel.IsQualified(requiredQuestLevel)) return;

            _inspectIndicator.SetBool(IsEnabled, false);
            LoadInspectTrigger(null);
        }

        private void LoadInspectTrigger(InspectTrigger trigger)
        {
            PlayerInspect.canInspect = trigger != null;
            PlayerInspect.loadedTrigger = trigger;
        }

    }
}