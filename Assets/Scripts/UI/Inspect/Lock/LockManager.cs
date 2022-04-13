using System;
using System.Collections;
using Character;
using Character.Player;
using TMPro;
using UnityEngine;

namespace UI.Inspect.Lock
{
    public class LockManager : MonoBehaviour
    {

        [Header("Interface Settings")]
        public TextMeshProUGUI currentSequenceUI;
        public Animator lockPad;
        public Animator startButton;
        
        [Header("Color Settings")]
        public Color normal;
        public Color success;
        public Color failure;

        private string _currentSequence;
        private string _correctSequence;
        private LockTrigger _trigger;
        private static readonly int IsEnabled = Animator.StringToHash("isEnabled");
        
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
            
            startButton.SetBool(IsEnabled, false);
            lockPad.SetBool(IsEnabled, true);
            _correctSequence = _trigger.correctSequence;
        }

        public void EnterKey(string character)
        {
            AudioManager.ButtonClick();
                
            _currentSequence += character;
            currentSequenceUI.SetText(_currentSequence);

            if (_currentSequence.Length < _correctSequence.Length) return;
            
            if (_currentSequence.Equals(_correctSequence)) StartCoroutine(Success());
            else StartCoroutine(Failure());
        }

        private IEnumerator Success()
        {
            AudioManager.Pause();
            
            currentSequenceUI.color = success;
            
            yield return new WaitForSeconds(1.0f);
            
            lockPad.SetBool(IsEnabled, false);
            
            yield return new WaitForSeconds(0.5f);
            
            Clear();
            if (_trigger.reward != null) _trigger.reward.SetActive(true);
            EndLock();
        }

        private IEnumerator Failure()
        {
            AudioManager.Decline();
            
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
            PlayerInspect.EndInspect();
            startButton.SetBool(IsEnabled, _trigger.shouldEnablePost);
            lockPad.SetBool(IsEnabled, false);
        }
        
    }
}