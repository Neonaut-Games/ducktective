using System;
using System.Collections;
using System.Collections.Generic;
using Character.Player;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
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

        [Header("Audio Settings")]
        [FormerlySerializedAs("characterSound")] public AudioSource messageSound;
        public AudioSource[] voicePlayer;
        public AudioSource[] voiceMom;
        public AudioSource[] voiceEddy;
        public AudioSource[] voiceRandy;
        public AudioSource[] voiceQuackintinius;
        public AudioSource[] voiceMeemaw;
        public AudioSource[] voiceBoss;
        
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
        public void StartDialogue(Dialogue dialogue)
        {
            DuckLog.Normal("A new dialogue is being initiated.");
            
            // Enable inspection mode for the player
            _playerInspect.BeginInspect();

            // Queue each dialogue entry from package into the messages
            _messageQueue.Clear();
            foreach (DialogueElement entry in dialogue.GetPackage())
            {
                DuckLog.Normal("Queueing Dialogue Element >> Author: {" + entry.GetAuthor() + "}, Message: {" + entry.GetMessage() + "}, Voice: {" + entry.GetVoice() + "}");
                _messageQueue.Enqueue(entry);
            }
            
            ShowUI(true);
            NextDialogueElement();
        }

        // Toggles the inspect indicator and dialogue box
        private void ShowUI(bool activity)
        {
            startButton.SetBool("isEnabled", !activity);
            dialogueBox.SetBool("isOpen", activity);
        }
        
        public void NextDialogueElement()
        {
            // Stop playing the character typing sound
            StopAllCoroutines();
            messageSound.Stop();
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
                    voice = voiceBoss;
                    break;
                case VoiceType.Eddy:
                    voice = voiceEddy;
                    break;
                case VoiceType.Meemaw:
                    voice = voiceMeemaw;
                    break;
                case VoiceType.Mom:
                    voice = voiceMom;
                    break;
                case VoiceType.Player:
                    voice = voicePlayer;
                    break;
                case VoiceType.Quackintinius:
                    voice = voiceQuackintinius;
                    break;
                case VoiceType.Randy:
                    voice = voiceRandy;
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
                if (!messageSound.isPlaying) messageSound.Play();
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
            messageSound.Stop();
            if (_currentVoice != null) _currentVoice.Stop();
            
            // Disable inspection mode for the player
            _playerInspect.EndInspect();

            ShowUI(false);
        
            // Set the player's quest level if applicable
            if (PlayerInspect.loadedTrigger == null)
                throw new NullReferenceException("No dialogue was loaded, but a sequence was triggered.");
            if (PlayerInspect.loadedTrigger.shouldChangeQuestLevel) PlayerLevel.SetLevel(PlayerInspect.loadedTrigger.rewardedQuestLevel);
            
            // Set gameObject to active if applicable
            if (PlayerInspect.loadedTrigger.rewardObject != null) PlayerInspect.loadedTrigger.rewardObject.SetActive(true);
        }

    }
}
