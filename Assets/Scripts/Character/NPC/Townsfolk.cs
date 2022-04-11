using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Serialization;

namespace Character.NPC
{
    public class Townsfolk : Mortal
    {

        [Header("Loot Settings")]
        [CanBeNull] public GameObject loot;
        [FormerlySerializedAs("lootMaximumAmount")] public int lootAmount;
        public GameObject gameObjectReward;

        protected override void OnDeath()
        {
            if (loot == null) return;

            var lootPosition = GetComponent<Renderer>().bounds.center;
            for (int i = 0; i < lootAmount; i++) Instantiate(loot, lootPosition, Quaternion.identity);

            if (gameObjectReward != null) gameObjectReward.SetActive(true);
        }
        
    }
}