using UnityEngine;

namespace Character.NPC.Townsfolk
{
    public class TownsfolkHit : StateMachineBehaviour
    {
        
        private Transform _player;
        private Rigidbody _rigidbody;
        
        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _player = GameObject.FindGameObjectWithTag("Player").transform;
            _rigidbody = animator.GetComponentInParent<Rigidbody>();
            
            /* Find the player and begin moving towards him. This snippet
            also forces the boss to always look at the player. */
            var playerPosition = _player.position;
            var npcPosition = _rigidbody.position;
            
            Vector3 destination = new Vector3(playerPosition.x, npcPosition.y, playerPosition.z);
            _rigidbody.gameObject.transform.LookAt(destination);
        }
    
    }
}
