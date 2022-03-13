using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public Animator dialogueBox;
    public TextMeshProUGUI messageElement;
    public TextMeshProUGUI authorElement;
    
    private Queue<string> _messages;
    
    void Start()
    {
        _messages = new Queue<string>();
    }

    private void ShowElements(bool activity)
    {
        //dialogueBox.gameObject.SetActive(activity);
        dialogueBox.SetBool("isOpen", activity);
    }

    IEnumerator AnimateMessage(string  message)
    {
        messageElement.SetText("");
        foreach (char character in message)
        {
            messageElement.SetText(messageElement.text + character);
            yield return new WaitForSeconds(0.025f);
        }
    }
    
    public void StartDialogue(Dialogue dialogue)
    {
        Debug.Log("Dialogue with " +  dialogue.author + " was initiated.");
        
        authorElement.SetText(dialogue.author);
        
        _messages.Clear();
        foreach (string message in dialogue.messages)
        {
            _messages.Enqueue(message);
        }

        DisplayNextMessage();
        ShowElements(true); // Make dialogue elements visible.
    }

    public void DisplayNextMessage()
    {
        // If there are no messages left, end the dialogue
        if (_messages.Count < 1)
        {
            EndDialogue();
            return;
        }

        string message = _messages.Dequeue();

        // Animate the next message onto the TextMeshProUI element (messageElement)
        Debug.Log("Dialogue >> " + message);
        StopAllCoroutines(); // Debug in-case player continues without typing finishing.
        StartCoroutine(AnimateMessage(message));
    }

    public void EndDialogue()
    {
        Debug.Log("Dialogue was was abandoned/ended.");
        ShowElements(false); // Make dialogue elements invisible.
    }

}
