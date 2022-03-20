using UnityEngine;

public class TakeCoins : MonoBehaviour
{
    private void Awake()
    {
        Items.Coin.amount -= 100;
        FindObjectOfType<CoinsManager>().RefreshAmount();
    }
}
