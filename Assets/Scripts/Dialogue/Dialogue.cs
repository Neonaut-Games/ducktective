using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking.PlayerConnection;

[Serializable]
public class Dialogue
{
    
    [TextArea(3, 10)] [SerializeField] private string[] messages;
    
    public Dictionary<int, KeyValuePair<string, string>> GetPackage()
    {
        Dictionary<int, KeyValuePair<string, string>> dialogue = new Dictionary<int, KeyValuePair<string, string>>();

        for(int i = 0; i < messages.Length; i++)
        {
            // Initialize current message entry
            string messageEntry = messages[i];
            
            // Set the author to the first word of the message
            string authorEntry = messageEntry.Split(' ').First();

            // Remove the author from the message itself
            messageEntry = messageEntry.Substring(authorEntry.Length + 1);
            
            // Add the current message's author and message to the dictionary
            dialogue.Add(i, new KeyValuePair<string, string>(authorEntry, messageEntry));
            Debug.Log("PACKAGING DIALOGUE ENTRY >> Key = {" + authorEntry + "}, Value=  {" + messageEntry + "}");
        }
        return dialogue;
    }
}
