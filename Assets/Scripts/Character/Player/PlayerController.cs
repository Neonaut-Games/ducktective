using System;
using System.Collections;
using UI.Menus;
using UnityEngine;
using UnityEngine.Serialization;

namespace Character.Player
{
    public class PlayerController : MonoBehaviour
    {
        [FormerlySerializedAs("_characterController")] public CharacterController characterController;
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
        public GameObject attackArrow;
        public LayerMask damageableLayer;
        public int damage = 10;
        public float attackCooldown = 1.0f;
        public AudioSource attackSound;
        
        [Header("Animation Settings")]
        public Animator playerAnimator;

        private static readonly int MovementState = Animator.StringToHash("movementState");
        private static readonly int Attacking = Animator.StringToHash("attacking");

        private void Start() => characterController = GetComponent<CharacterController>();

        private void Update()
        {
            // Set the player's movement state == 0 ("idle")
            playerAnimator.SetInteger(MovementState, 0);
            
            /* If the player is currently restricted in
            some way, ignore their movement entirely. */
            if (!characterController.enabled) return;
            if (PlayerInspect.movementRestricted) return;
            if (!PlayerHealth.isAlive) return;
            if (PauseMenu.isPaused) return;
            
            Move();
            Gravity();
            Combat();
        }

        #region Movement Mechanics

        private void Move()
        {

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
                    playerAnimator.SetInteger(MovementState, 2);
                }
                else
                {
                    _sprintAdditive = 1.0f;
                    // Set the player's movement state == 1 ("walking")
                    playerAnimator.SetInteger(MovementState, 1);
                }

                // Move the player along the X, Z 
                characterController.Move(movementDirectionModded.normalized * (movementMultiplier * _sprintAdditive * Time.deltaTime));
            }

        }

        private void Gravity() => characterController.Move(Physics.gravity * Time.deltaTime);

        #endregion

        #region Combat Mechanics

        private void Combat()
        {
            // Initiate an attack
            if (Input.GetKeyDown(KeyCode.Mouse0)) StartCoroutine(Attack());
            
            // Show and/or hide attack indicator
            if (Input.GetKey(KeyCode.Mouse1)) attackArrow.SetActive(true);
            else if (attackArrow.activeSelf) attackArrow.SetActive(false);
        }

        private IEnumerator Attack()
        {
            // Restrict the player's movement while attacking
            PlayerInspect.movementRestricted = true;

            // Set the player's animator to be attacking
            playerAnimator.SetTrigger(Attacking);

            // Deal damage at the transform point
            var results = Physics.OverlapSphere(damagePoint.position, attackRange, damageableLayer.value);
            if (results.Length > 0)
            {
                PlayerStats.totalHits++;
                DamageEntities(results);
            }
            else
            {
                PlayerStats.totalMisses++;
                attackSound.Play(); // Play attack miss sound
            }

            /* Wait for enough of the animation to finish so that
            it's not super odd looking if they attack again. This
            serves as a sort of "attack cooldown". */
            yield return new WaitForSeconds(attackCooldown);
            
            // Restrict the player's movement while attacking
            PlayerInspect.movementRestricted = false;
        }

        private void DamageEntities(Collider[] entities)
        {
            // Have each entity take damage
            foreach (Collider entity in entities)
            {
                try
                {
                    var wasKilled = entity.GetComponent<Mortal>().TakeDamage(damage);
                    if (wasKilled) PlayerStats.totalKills++;
                    DuckLog.Normal("The player attacked " + entity.name);
                }
                catch (NullReferenceException) { }
            }
        }

        #endregion

    }
}
