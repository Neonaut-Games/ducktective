using System;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    
    private Animator inspectIndicator;
    
    [Header("Dialogue Settings")]
    public Dialogue dialogue;

    [Header("Trigger Requirements")]
    public bool shouldRequireQuestLevel;
    public int requiredQuestLevel;
    
    [Header("Completion Rewards")]
    public bool shouldChangeQuestLevel;
    public int rewardedQuestLevel;

    public void Start()
    {
        inspectIndicator = GameObject.FindGameObjectWithTag("InspectIndicator").GetComponent<Animator>();
    }

    public void TriggerDialogue()
    {
        Debug.Log("Dialogue was triggered by " + gameObject.name);
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        // If the object is not a player, ignore the event.
        if (!other.CompareTag("Player")) return;
        
        // If the player does not have the required quest level, ignore the event.
        if (shouldRequireQuestLevel) if (PlayerLevel.GetLevel() != requiredQuestLevel) return;
        
        inspectIndicator.SetBool("isEnabled", true);
        PlayerInspect.canInspect = true;
        PlayerInspect.loadedTrigger = this;
    }

    private void OnTriggerExit(Collider other)
    {
        // If the object is not a player, ignore the event.
        if (!other.CompareTag("Player")) return;
        
        // If the player does not have the required quest level, ignore the event.
        if (shouldRequireQuestLevel) if (PlayerLevel.GetLevel() != requiredQuestLevel) return;

        inspectIndicator.SetBool("isEnabled", false);
        PlayerInspect.canInspect = false;
        PlayerInspect.loadedTrigger = null;
    }

}
