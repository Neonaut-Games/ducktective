using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UI.Dialogue
{
    [Serializable]
    public class Dialogue
    {
        public Dialogue(String[] messages, VoiceType[] voices)
        {
            this.messages = messages;
            this.voices = voices;
        }

        [TextArea(3, 10)] [SerializeField] private string[] messages;
        [SerializeField] private VoiceType[] voices;

        /* This function packages the separate, editor-defined arrays into
        a single Dictionary object with several nested objects inside. */
        public List<DialogueElement> GetPackage()
        {

            /* Ensure that the length of both messages[] and
            voices[] are the same so that all displayed messages 
            can be voiced over during the dialogue sequence. */
            if (voices.Length != messages.Length)
            {
                throw new ArgumentException(
                    "Unequal amount of messages and voices; dialogue could not be packaged.");
            }

            List<DialogueElement> dialogue = new List<DialogueElement>();

            foreach (var message in messages)
            {
                // Initialize current message entry
                var messageEntry = message;
                
                // Set the author to the first word of the message
                var authorEntry = messageEntry.Split(' ').First();

                // Remove the author from the message itself
                messageEntry = messageEntry.Substring(authorEntry.Length + 1);
                
                // Add the current message's author and message to the dictionary
                dialogue.Add(new DialogueElement(authorEntry, messageEntry, VoiceType.Player));
                Debug.Log("PACKAGING DIALOGUE ENTRY >> Author: {" + authorEntry + "}, Message: {" + messageEntry + "}, Voice: {" + null + "}");

            }
            
            return dialogue;
        }
    }
}
