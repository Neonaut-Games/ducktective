using System;
using Character;
using Character.Player;
using UnityEngine;

namespace UI.Inspect
{
    public abstract class InspectTrigger : MonoBehaviour
    {
        
        
        [Header("Trigger Requirements")]
        public bool shouldRequireQuestLevel;
        public int requiredQuestLevel;

        public abstract void Trigger();


        private void OnTriggerEnter(Collider other)
        {
            // If the object is not a player, ignore the event.
            if (!other.CompareTag("Player")) return;
            
            // If the player does not have the required quest level, ignore the event.
            if (shouldRequireQuestLevel)
            {
                if (PlayerLevel.questLevel != requiredQuestLevel) return;
            }
            
            InspectIcon.Enable();
        }

        private void OnTriggerStay(Collider other)
        {
            // If the object is not a player, ignore the event.
            if (!other.CompareTag("Player")) return;

            // If the player does not have the required quest level, ignore the event.
            if (shouldRequireQuestLevel)
            {
                if (PlayerLevel.questLevel != requiredQuestLevel) return;
            }
            LoadInspectTrigger(this);
        }

        private void OnTriggerExit(Collider other)
        {
            // If the object is not a player, ignore the event.
            if (!other.CompareTag("Player")) return;

            InspectIcon.Disable();
            LoadInspectTrigger(null);
        }

        private void LoadInspectTrigger(InspectTrigger trigger)
        {
            PlayerInspect.canInspect = trigger != null;
            PlayerInspect.loadedTrigger = trigger;
        }

    }
}