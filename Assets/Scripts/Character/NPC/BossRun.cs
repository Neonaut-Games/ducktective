using UnityEngine;

namespace Character.NPC
{ 
    public class BossRun : StateMachineBehaviour
    {
        private Transform _player;
        private Rigidbody _rigidbody;

        [Header("Movement Settings")]
        public float speed = 0.75f;
        
        [Header("Combat Settings")]
        public float attackRange = 3.0f;

        private static readonly int Attack = Animator.StringToHash("attack");

        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _player = GameObject.FindGameObjectWithTag("Player").transform;
            _rigidbody = animator.GetComponentInParent<Rigidbody>();
        }

        // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            /* Find the player and begin moving towards him. This snippet
            also forces the boss to always look at the player. */
            var playerPosition = _player.position;
            var bossPosition = _rigidbody.position;

            Vector3 destination = new Vector3(playerPosition.x, bossPosition.y, playerPosition.z);
            Vector3 towardsPosition = Vector3.MoveTowards(bossPosition, destination, speed * Time.fixedDeltaTime);
            _rigidbody.transform.LookAt(towardsPosition);
            
            /* If the boss is within the defined attack range of the
             player, stop running and switch to the attack phase. */
            if (Vector3.Distance(playerPosition, bossPosition) <= attackRange)
            {
                animator.SetTrigger(Attack);
            }
            else
            {
                _rigidbody.MovePosition(towardsPosition);
            }
        }

        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.ResetTrigger(Attack);
        }
    }
}
