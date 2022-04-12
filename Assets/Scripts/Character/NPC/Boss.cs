using Character.Player;
using JetBrains.Annotations;
using UnityEngine;
using Debug = System.Diagnostics.Debug;

namespace Character.NPC
{
    public class Boss : Mortal
    {
    
        [Header("Combat Settings")]
        public int sightRange = 30;
        
        [Header("Loot Settings")]
        [CanBeNull] public GameObject loot;

        private static readonly int IsMoving = Animator.StringToHash("isMoving");

        public void Update()
        {
            var playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
            var bossPosition = mortalRigidbody.position;


            if (Vector3.Distance(playerPosition, bossPosition) > sightRange)
            {
                animator.SetBool(IsMoving, false);
            }
            else
            {
                animator.SetBool(IsMoving, true);
            }
        }

        protected override void OnDeath()
        {
            if (loot == null) return;
            
            var lootPosition = transform.position + Vector3.up;
            Debug.Assert(loot != null, nameof(loot) + " != null");
            Instantiate(loot, lootPosition, loot.transform.rotation);
            
            PlayerLevel.SetQuestLevel(9);
        }
    }
}
