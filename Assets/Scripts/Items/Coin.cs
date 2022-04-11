using Character;
using UI.Uninteractable;
using UnityEngine;

namespace Items
{
    public class Coin : ConsumableItem
    {

        [Header("Coin Settings")]
        public int value;
        public override void Consume()
        {
            AudioManager.Coin();
            CoinsManager.Deposit(value);
        }
    }
}
