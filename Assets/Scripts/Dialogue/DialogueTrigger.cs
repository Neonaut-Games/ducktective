using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    public void TriggerDialogue()
    {
        Debug.Log("Dialogue was triggered by " + gameObject.name);
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

}
