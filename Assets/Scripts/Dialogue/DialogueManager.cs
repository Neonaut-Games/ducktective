using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Dialogue
{
    public class DialogueManager : MonoBehaviour
    {
        [Header("Component Settings")]
        public Animator dialogueBox;
        public Animator startButton;
        public TextMeshProUGUI messageElement;
        public TextMeshProUGUI authorElement;
        public AudioSource characterSound;

        private Dictionary<int, KeyValuePair<string, string>> _dialoguePackage;
        private Queue<KeyValuePair<string, string>> _messageQueue;

        // Initializes private variables
        private void Start()
        {
            _messageQueue = new Queue<KeyValuePair<string, string>>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space)) EndDialogue();
        }

        // Shows & hides dialogue elements all at once
        private void ShowElements(bool activity)
        {
            startButton.SetBool("isEnabled", !activity);
            dialogueBox.SetBool("isOpen", activity);
        }

        // Gives the "write-on" animation for each message
        private IEnumerator AnimateMessage(string  message)
        {
            messageElement.SetText("");
            foreach (char character in message)
            {
                if (!characterSound.isPlaying) characterSound.Play();
                messageElement.SetText(messageElement.text + character);
                yield return new WaitForSeconds(0.025f);
            }
        }
    
        /* !START! - Initiates the dialogue sequence.
        This function is called by DialogueTrigger only. */
        public void StartDialogue(Dialogue dialogue)
        {
            // Enable inspection mode for the player
            FindObjectOfType<PlayerInspect>().BeginInspect();

            // Get packaged dialogue
            _dialoguePackage = dialogue.GetPackage();

            // Log to console
            Debug.Log("A new dialogue is being initiated.");

            // Queue each dialogue entry from package into the messages
            _messageQueue.Clear();
            foreach (KeyValuePair<string, string> entry in _dialoguePackage.Values)
            {
                Debug.Log("QUEUEING DIALOGUE ENTRY >> Key = {" + entry.Key + "}, Value=  {" + entry.Value + "}");
                _messageQueue.Enqueue(entry);
            }

            // Make dialogue elements visible to the user
            ShowElements(true); 
        
            // Display & animate dialogue to the user
            DisplayNextMessage();
        }

        public void DisplayNextMessage()
        {
            // Stop playing the character typing sound
            StopAllCoroutines();
            characterSound.Stop();
        
            // If there are no messages left in the queue, end the dialogue
            if (_messageQueue.Count == 0)
            {
                EndDialogue();
                return;
            }
        
            // Load the current message's author and message
            KeyValuePair<string, string> subpackage = _messageQueue.Dequeue();
            string author = subpackage.Key;
            string message = subpackage.Value;

            // Log the message to the console
            Debug.Log("Dialogue >> " + author + ": " + message);
        
            // Set the current author
            authorElement.SetText(author);

            // Animate the next message onto the TextMeshProUI element (messageElement)
            StopAllCoroutines(); // Debug in-case player continues without typing finishing.
            StartCoroutine(AnimateMessage(message));
        }

        private void EndDialogue()
        {
            // Stop playing the character typing sound if they skipped the last message
            StopAllCoroutines();
            characterSound.Stop();
            
            // Disable inspection mode for the player
            FindObjectOfType<PlayerInspect>().EndInspect();
        
            Debug.Log("Dialogue was was abandoned/ended.");
            ShowElements(false); // Make dialogue elements invisible.
        
            // Set the player's quest level if applicable
            if (PlayerInspect.loadedTrigger.shouldChangeQuestLevel) PlayerLevel.SetLevel(PlayerInspect.loadedTrigger.rewardedQuestLevel);
            
            // Set gameObject to active if applicable
            if (PlayerInspect.loadedTrigger.rewardFunction != null) PlayerInspect.loadedTrigger.rewardFunction.SetActive(true);
        }

    }
}
