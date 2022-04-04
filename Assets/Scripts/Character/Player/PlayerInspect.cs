using System;
using Cinemachine;
using JetBrains.Annotations;
using UI;
using UnityEngine;

namespace Character.Player
{
    public class PlayerInspect : MonoBehaviour
    {
        public static bool movementRestricted;
        public static bool canInspect;
        [CanBeNull] public static InspectTrigger loadedTrigger = null;

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
                    DuckLog.Normal("Player began inspecting something.");
                    if (loadedTrigger == null)
                    {
                        Debug.LogError("Player attempted inspect but loadedTrigger was not assigned.");
                        return;
                    }

                    try { loadedTrigger.Trigger(); } catch (NullReferenceException) { }

                } else DuckLog.Normal("No inspect available right now.");
            }
        }

        public void BeginInspect()
        {
            // Swap to still-positioned camera and disable Cinemachine follow camera
            var followCameraTransform = followCamera.transform;
            stillCamera.transform.SetPositionAndRotation(followCameraTransform.position, followCameraTransform.rotation);
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
