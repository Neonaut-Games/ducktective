using Cinemachine;
using JetBrains.Annotations;
using UnityEngine;

public class PlayerInspect : MonoBehaviour
{
    public static bool isInspecting;
    public static bool canInspect;
    [CanBeNull] public static DialogueTrigger loadedTrigger = null;

    [Header("Camera Settings")]
    public Camera stillCamera;
    public CinemachineBrain followCamera;

    private void Update()
    {
        if (Input.GetKey(KeyCode.I) && !isInspecting)
        {
            Debug.Log("Player attempted to perform inspect");
            if (canInspect)
            {
                Debug.Log("Inspect initiated.");
                if (loadedTrigger == null)
                {
                    Debug.LogError("Player attempted inspect but loadedDialogue was not assigned.");
                    return;
                }
                loadedTrigger.TriggerDialogue();
            }
            else
            {
                Debug.Log("No inspect available right now.");
            }
        }
    }

    public void BeginInspect()
    {
        stillCamera.transform.SetPositionAndRotation(followCamera.transform.position, followCamera.transform.rotation);
        followCamera.gameObject.SetActive(false);
        stillCamera.gameObject.SetActive(true);
        isInspecting = true;
    }

    public void EndInspect()
    {
        stillCamera.gameObject.SetActive(false);
        followCamera.gameObject.SetActive(true);
        isInspecting = false;
    }

}
