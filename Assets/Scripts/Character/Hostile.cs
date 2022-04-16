using UnityEngine;

namespace Character
{
    public abstract class Hostile : Mortal
    {
        
        [Header("Combat Settings")]
        public int sightRange = 30;


        [HideInInspector] public bool aggro;
        private static readonly int IsMoving = Animator.StringToHash("isMoving");

        protected override void AfterStart() {}

        public void Update()
        {
            if (!isAlive) return;
            
            var playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
            var mortalPosition = mortalRigidbody.position;

            if (Vector3.Distance(playerPosition, mortalPosition) <= sightRange)
            {
                /* If the player is within range, the boss will
                become hostile and begin chasing the player. */
                aggro = true;
                animator.SetBool(IsMoving, true);
                OnAggro();
            }
            else if (aggro)
            {
                /* If the player is not within range, the boss
                becomes idle and does not chase the player. */
                aggro = false;
                animator.SetBool(IsMoving, false);
                OnDeAggro();
            }
        }

        protected abstract void OnAggro();
        
        protected abstract void OnDeAggro();

    }
}