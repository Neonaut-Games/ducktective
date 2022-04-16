using Character.Player;
using JetBrains.Annotations;
using UnityEngine;
using Debug = System.Diagnostics.Debug;

namespace Character.NPC.Temeduck
{
    public class Temeduck : Boss
    {
        
        [Header("Loot Settings")]
        [CanBeNull] public GameObject loot;

        private static readonly int Enabled = Animator.StringToHash("isEnabled");
        
        protected override void OnDeath()
        {
            healthBarAnimator.SetBool(Enabled, false);
            
            if (loot == null) return;
            var lootPosition = transform.position + Vector3.up;
            Debug.Assert(loot != null, nameof(loot) + " != null");
            Instantiate(loot, lootPosition, loot.transform.rotation);
            
            PlayerLevel.SetQuestLevel(9);
        }
        
    }
}
