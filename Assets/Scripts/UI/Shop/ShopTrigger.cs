using JetBrains.Annotations;
using UnityEngine;

namespace UI.Shop
{
    public class ShopTrigger : InspectTrigger
    {

        [Header("Shop Settings")]
        public Shop shop;
        
        [Header("Purchase Rewards")]
        public bool shouldChangeQuestLevel;
        public int rewardedQuestLevel;
        [CanBeNull] public GameObject rewardObject;
        
        public override void Trigger()
        {
            DuckLog.Normal("Shop was triggered by " + gameObject.name);
            FindObjectOfType<ShopManager>().StartShop(shop, this);
        }
        
    }
}
