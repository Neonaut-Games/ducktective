using Cinemachine;
using Dialogue;
using JetBrains.Annotations;
using UnityEngine;

namespace Player
{
    public class PlayerInspect : MonoBehaviour
    {
        public static bool movementRestricted;
        public static bool canInspect;
        [CanBeNull] public static DialogueTrigger loadedTrigger = null;

        [Header("Camera Settings")]
        public Camera stillCamera;
        public CinemachineBrain followCamera;

        private void Update()
        {
            /* If the player is pressing "I" and is not already movementRestricted
             (detects if they are already inspecting something), attempt inspect. */
            if (Input.GetKeyDown(KeyCode.I) && !movementRestricted)
            {
                // If the player can currently inspect something, perform inspect.
                if (canInspect)
                {
                    Debug.Log("Player began inspecting something.");
                    if (loadedTrigger == null)
                    {
                        Debug.LogError("Player attempted inspect but loadedDialogue was not assigned.");
                        return;
                    }
                    loadedTrigger.TriggerDialogue();
                }
                else Debug.Log("No inspect available right now.");
            }
        }

        public void BeginInspect()
        {
            // Swap to still-positioned camera and disable Cinemachine follow camera
            stillCamera.transform.SetPositionAndRotation(followCamera.transform.position, followCamera.transform.rotation);
            followCamera.gameObject.SetActive(false);
            stillCamera.gameObject.SetActive(true);
            
            // Restrict the player's movement
            movementRestricted = true;
        }

        public void EndInspect()
        {
            // Swap to Cinemachine follow camera and disable still-positioned camera
            stillCamera.gameObject.SetActive(false);
            followCamera.gameObject.SetActive(true);
            
            // Restrict the player's movement
            movementRestricted = false;
        }

    }
}
