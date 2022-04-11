using Character.Player;
using UI;
using UnityEngine;

namespace Quests
{
    public class TakeCoins : MonoBehaviour
    {
        private void OnEnable()
        {
            Items.Coin.amount -= 50;
            FindObjectOfType<CoinsManager>().RefreshAmount();
        }
    }
}
