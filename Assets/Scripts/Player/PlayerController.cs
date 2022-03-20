using System;
using System.Collections;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        private CharacterController _characterController;

        [Header("Camera Settings")] public Transform playerCamera;
        [Range(0, 1)] public float rotationSmoothTime = 0.1f;
        private float _turnSmoothVelocity;

        [Header("Movement Settings")] 
        [Range(1, 50)] public float movementMultiplier = 5;
        public float sprintMultiplier = 1.25f;
        private float _sprintAdditive = 1.0f;

        [Header("Combat Settings")]
        public Transform damagePoint;
        public float attackRange;
        public LayerMask damageableLayer;
        public int damage = 10;
        public float attackCooldown = 0.5f;
        
        [Header("Animation Settings")]
        public Animator playerAnimator;

        void Start()
        {
            _characterController = GetComponent<CharacterController>();
        }

        void Update()
        {
            Move();
            Combat();
        }

        #region Movement Mechanics

        public void Move()
        {
            
            // Set the player's movement state == 0 ("idle")
            playerAnimator.SetInteger("movementState", 0);

            // If the player is currently inspecting something, ignore their movement input
            if (PlayerInspect.movementRestricted) return;

            // Get the movement inputs from the W, A, S, D, and space keys
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
                transform.rotation = Quaternion.Euler(0f, smoothedAngle, 0);

                // Modify movement to be in the direction of the camera
                Vector3 movementDirectionModded = Quaternion.Euler(0f, (float) targetAngle, 0f) * Vector3.forward;

                // Check if player is sprinting and update _sprintAdditive accordingly
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    _sprintAdditive = sprintMultiplier;
                    // Set the player's movement state == 2 ("running")
                    playerAnimator.SetInteger("movementState", 2);
                }
                else
                {
                    _sprintAdditive = 1.0f;
                    // Set the player's movement state == 1 ("walking")
                    playerAnimator.SetInteger("movementState", 1);
                }

                // Move the player along the X, Z 
                _characterController.Move(movementDirectionModded.normalized * movementMultiplier * _sprintAdditive * Time.deltaTime);
            }
            
            if (!_characterController.isGrounded)
            {
                _characterController.Move(Physics.gravity * Time.deltaTime);
            }
        }

        #endregion

        #region Combat Mechanics

        public void Combat()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (!PlayerInspect.movementRestricted) StartCoroutine(Attack());
            }
        }

        public IEnumerator Attack()
        {
            // Restrict the player's movement while attacking
            PlayerInspect.movementRestricted = true;
            
            // Set the player's animator to be attacking
            playerAnimator.SetBool("isAttacking", true);

            // Deal damage at the transform point
            Collider[] results = Physics.OverlapSphere(damagePoint.position, attackRange, damageableLayer.value);
            foreach (Collider damagedEntity in results)
            {
                Debug.Log("Player damaged " + damagedEntity.name);
                damagedEntity.GetComponent<DamageableEntity>().TakeDamage(damage);
            }
            
            /* Wait for enough of the animation to finish so that
            it's not super odd looking if they attack again. This
            serves as a sort of "attack cooldown". */
            yield return new WaitForSeconds(attackCooldown);
            
            // Restrict the player's movement while attacking
            PlayerInspect.movementRestricted = false;
            
            // Set the player's animator to be attacking
            playerAnimator.SetBool("isAttacking", false);
        }

        #endregion

    }
}
