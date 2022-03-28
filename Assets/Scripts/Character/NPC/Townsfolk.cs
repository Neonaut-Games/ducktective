using JetBrains.Annotations;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Character.NPC
{
    public class Townsfolk : Mortal
    {
        
        [Header("Loot Settings")] [CanBeNull] public GameObject loot;
        public int lootMinimumAmount;
        public int lootMaximumAmount;
        
        public void DeathReward()
        {
            if (loot == null) return;
            for (int i = 0; i < Random.Range(lootMinimumAmount, lootMaximumAmount); i++)
            {
                Instantiate(loot, transform.position, Quaternion.identity);
            }
        }
        
    }
}