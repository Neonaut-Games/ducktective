using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController _characterController;
    
    [Header("Camera Settings")]
    public Transform playerCamera;
    [Range(0, 1)] public float rotationSmoothTime = 0.1f;
    private float _turnSmoothVelocity;
    
    [Header("Player Movement Settings")]
    [Range(1, 50)] public float movementMultiplier = 5;
    public float gravityStrength = 0.5f;
    public float jumpForce = 3.0f;
    public float sprintMultiplier = 1.25f;
    private float _sprintAdditive = 1.0f;

    void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }
    
    void Update()
    {

        if (PlayerInspect.isInspecting) return;
        
        // Get the movement inputs from the W, A, S, D keys
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 movementDirection = new Vector3(horizontal, 0f, vertical).normalized;

        // If the player is attempting to move at all
        if (movementDirection.magnitude >= 0.1f)
        {
            /* Get the angle the player should be facing based on what
            direction they're currently moving towards (face forwards!) */
            double targetAngle = Math.Atan2(movementDirection.x, movementDirection.z) * Mathf.Rad2Deg + playerCamera.eulerAngles.y;
            // Smooth the angle so it's not snap-to-grid
            float smoothedAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, (float) targetAngle, ref _turnSmoothVelocity, rotationSmoothTime);
            // Rotate the player towards the direction they're moving
            transform.rotation = Quaternion.Euler(0f,  smoothedAngle, 0);
            
            // Modify movement to be in the direction of the camera
            Vector3 movementDirectionModded = Quaternion.Euler(0f, (float) targetAngle, 0f) * Vector3.forward;
            
            // Check if player is sprinting and update _sprintAdditive accordingly
            _sprintAdditive = Input.GetKey(KeyCode.LeftShift) ? sprintMultiplier : 1.0f;
            
            // Move the player along the X, Z 
            _characterController.Move(movementDirectionModded.normalized * movementMultiplier * _sprintAdditive * Time.deltaTime);
        }

        if (!_characterController.isGrounded)
        {
            _characterController.SimpleMove(new Vector3(0f, gravityStrength * -1, 0f));
        }

    }
}
