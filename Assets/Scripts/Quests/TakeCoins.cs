using UI.Uninteractable;
using UnityEngine;

namespace Quests
{
    public class TakeCoins : MonoBehaviour
    {
        private void OnEnable()
        {
            CoinsManager.Withdraw(50);
            CoinsManager.RefreshBalance();
        }
    }
}
