using System;
using Cinemachine;
using JetBrains.Annotations;
using UI.Inspect;
using UnityEngine;

namespace Character.Player
{
    public class PlayerInspect : MonoBehaviour
    {
        private static PlayerInspect _instance;
        
        public static bool movementRestricted;
        public static bool canInspect;
        [CanBeNull] public static InspectTrigger loadedTrigger = null;

        [Header("Camera Settings")]
        public Camera stillCamera;
        public CinemachineBrain followCamera;

        private void Start() => _instance = this;

        private void Update()
        {
            /* If the player is pressing "E" and is not already movementRestricted
             (detects if they are already inspecting something), attempt inspect. */
            if (Input.GetKeyDown(KeyCode.E) && !movementRestricted)
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

        public static void BeginInspect()
        {
            // Swap to still-positioned camera and disable Cinemachine follow camera
            var followCameraTransform = _instance.followCamera.transform;
            _instance.stillCamera.transform.SetPositionAndRotation(followCameraTransform.position, followCameraTransform.rotation);
            _instance.followCamera.gameObject.SetActive(false);
            _instance.stillCamera.gameObject.SetActive(true);
            
            // Restrict the player's movement
            movementRestricted = true;
        }

        public static void EndInspect()
        {
            // Swap to Cinemachine follow camera and disable still-positioned camera
            _instance.stillCamera.gameObject.SetActive(false);
            _instance.followCamera.gameObject.SetActive(true);
            
            // Restrict the player's movement
            movementRestricted = false;
        }

    }
}
