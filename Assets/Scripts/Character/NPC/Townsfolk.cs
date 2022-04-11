using JetBrains.Annotations;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Character.NPC
{
    public class Townsfolk : Mortal
    {
        
        [Header("Loot Settings")]
        [CanBeNull] public GameObject loot;
        public int lootMinimumAmount;
        public int lootMaximumAmount;
        public GameObject gameObjectReward;

        protected override void OnDeath()
        {
            if (loot == null) return;
            
            var lootPosition = transform.position;
            lootPosition.y += 1;
            
            for (int i = 0; i < Random.Range(lootMinimumAmount, lootMaximumAmount); i++)
            {
                Instantiate(loot, lootPosition, Quaternion.identity);
            }

            if (gameObjectReward != null) gameObjectReward.SetActive(true);
        }
        
    }
}