using System;
using System.Collections;
using System.Collections.Generic;
using Character;
using Character.Player;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace UI.Dialogue
{
    public class DialogueManager : MonoBehaviour
    {
        [Header("Component Settings")]
        public Animator dialogueBox;
        public Animator startButton;
        public TextMeshProUGUI messageElement;
        public TextMeshProUGUI authorElement;

        private DialogueTrigger _trigger;
        private Queue<DialogueElement> _messageQueue;
        private PlayerInspect _playerInspect;
        private AudioSource _currentVoice;
        
        private void Start()
        {
            _messageQueue = new Queue<DialogueElement>();
            _playerInspect = FindObjectOfType<PlayerInspect>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space) && PlayerInspect.movementRestricted) EndDialogue();
        }
        
        /* Initiates a given dialogue sequence. This function
        is called by DialogueTrigger and BlackFeather. */
        public void StartDialogue(Dialogue dialogue, DialogueTrigger trigger)
        {
            DuckLog.Normal("A new dialogue is being initiated.");
            
            // Load the trigger
            if (trigger == null)
                throw new NullReferenceException("No dialogue was loaded, but a sequence was triggered.");
            _trigger = trigger;

            // Enable inspection mode for the player
            _playerInspect.BeginInspect();

            // Queue each dialogue entry from package into the messages
            _messageQueue.Clear();
            foreach (DialogueElement entry in dialogue.GetPackage())
            {
                DuckLog.Normal("Queueing Dialogue Element >> Author: {" + entry.GetAuthor() + "}, Message: {" + entry.GetMessage() + "}, Voice: {" + entry.GetVoice() + "}");
                _messageQueue.Enqueue(entry);
            }
            
            startButton.SetBool("isEnabled", false);
            dialogueBox.SetBool("isOpen", true);
            NextDialogueElement();
        }
        
        public void NextDialogueElement()
        {
            // Stop playing the character typing sound
            StopAllCoroutines();
            AudioManager.a.messageSound.Stop();
            if (_currentVoice != null) _currentVoice.Stop();
        
            // If there are no messages left in the queue, end the dialogue
            if (_messageQueue.Count == 0)
            {
                EndDialogue();
                return;
            }
        
            /* Unpack the next DialogueElement in the queue
            and separate the author, message, and voice type. */
            DialogueElement subpackage = _messageQueue.Dequeue();
            var author = subpackage.GetAuthor();
            var message = subpackage.GetMessage();
            var voiceType = subpackage.GetVoice();
            AudioSource[] voice;
            switch (voiceType)
            {
                case VoiceType.Boss:
                    voice = AudioManager.a.voiceBoss;
                    break;
                case VoiceType.Eddy:
                    voice = AudioManager.a.voiceEddy;
                    break;
                case VoiceType.Meemaw:
                    voice = AudioManager.a.voiceMeemaw;
                    break;
                case VoiceType.Mom:
                    voice = AudioManager.a.voiceMom;
                    break;
                case VoiceType.Player:
                    voice = AudioManager.a.voicePlayer;
                    break;
                case VoiceType.Quackintinius:
                    voice = AudioManager.a.voiceQuackintinius;
                    break;
                case VoiceType.Randy:
                    voice = AudioManager.a.voiceRandy;
                    break;
                default:
                    throw new ArgumentException("The given voice has no sound available.");
            }
            
            DuckLog.Normal("Displaying Dialogue >> " + author + ": " + message);
            
            authorElement.SetText(author);
            StartCoroutine(PlayDialogueElement(message, voice));
        }

        /* Displays the "write-on" animation for each message along
        with the character typing sound and character-specific voice
        over effects. */
        private IEnumerator PlayDialogueElement(string message, AudioSource[] voice)
        {
            // Clear the previous message
            messageElement.SetText("");
            foreach (char character in message)
            {
                /* Animate message typing onto screen
                character-by-character with a typing sound. */
                if (!AudioManager.a.messageSound.isPlaying) AudioManager.a.messageSound.Play();
                messageElement.SetText(messageElement.text + character);
                
                /* If the previous voice-line is not playing, play a new
                random voice-line from the character's voice type. */
                if (_currentVoice == null || !_currentVoice.isPlaying)
                { 
                    _currentVoice = SelectRandomAudio(voice);
                    _currentVoice.Play();
                }

                yield return new WaitForSeconds(0.025f);
            }
        }
        
        private AudioSource SelectRandomAudio(AudioSource[] audioSources)
        {
            return audioSources[Random.Range(0, audioSources.Length)];
        }

        private void EndDialogue()
        {
            DuckLog.Normal("Dialogue was was abandoned/ended.");
            
            // Stop playing the character typing sound
            StopAllCoroutines();
            AudioManager.a.messageSound.Stop();
            if (_currentVoice != null) _currentVoice.Stop();
            
            // Disable inspection mode for the player
            _playerInspect.EndInspect();

            startButton.SetBool("isEnabled", _trigger.shouldEnablePost);
            dialogueBox.SetBool("isOpen", false);
        
            // Set the player's quest level if applicable
            if (_trigger.shouldChangeQuestLevel) PlayerLevel.SetQuestLevel(_trigger.rewardedQuestLevel);
            
            // Set gameObject to active if applicable
            if (_trigger.rewardObject != null) _trigger.rewardObject.SetActive(true);
        }

    }
}
