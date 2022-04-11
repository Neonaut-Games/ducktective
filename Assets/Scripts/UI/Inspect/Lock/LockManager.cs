using System;
using System.Collections;
using Character.Player;
using TMPro;
using UnityEngine;

namespace UI.Inspect.Lock
{
    public class LockManager : MonoBehaviour
    {
        private string _currentSequence;

        [Header("Interface Settings")]
        public TextMeshProUGUI currentSequenceUI;
        public string correctSequence;
        
        [Header("Color Settings")]
        public Color normal;
        public Color success;
        public Color failure;

        private LockTrigger _trigger;

        private void Update()
        {
            // If the player presses space at any time
            if (Input.GetKeyDown(KeyCode.Space)) {
                /* If the player is currently in inspection mod
                mode and there is a trigger currently loaded. */
                if (PlayerInspect.movementRestricted && PlayerInspect.loadedTrigger != null)
                {
                    // If the loaded trigger is a DialogueTrigger
                    if (PlayerInspect.loadedTrigger.GetType() == typeof(LockTrigger)) EndLock();
                }
            }
        }
        
        public void StartLock(LockTrigger trigger)
        {
            DuckLog.Normal("A new shop is being initiated.");
            
            // Load the trigger
            if (trigger == null)
                throw new NullReferenceException("No lock was loaded, but a sequence was triggered.");
            _trigger = trigger;

            // Enable inspection mode for the player
            PlayerInspect.BeginInspect();
            
            _trigger.lockUI.SetActive(true);
        }

        public void EnterKey(string character)
        {
            _currentSequence += character;
            currentSequenceUI.SetText(_currentSequence);

            if (_currentSequence.Length < 4) return;
            if (_currentSequence.Equals(correctSequence)) StartCoroutine(Success());
            else StartCoroutine(Failure());
        }

        private IEnumerator Success()
        {
            currentSequenceUI.color = success;
            yield return new WaitForSeconds(1.0f);
            Clear();
        }

        private IEnumerator Failure()
        {
            currentSequenceUI.color = failure;
            yield return new WaitForSeconds(1.0f);
            Clear();
        }

        public void Clear()
        {
            _currentSequence = "";
            currentSequenceUI.SetText(_currentSequence);
            currentSequenceUI.color = normal;
        }

        public void EndLock()
        {
            _trigger.lockUI.SetActive(false);
        }
        
    }
}