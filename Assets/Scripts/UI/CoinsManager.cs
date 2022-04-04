using System.Collections;
using Items;
using TMPro;
using UnityEngine;

namespace UI
{
    public class CoinsManager : MonoBehaviour
    {
        public TextMeshProUGUI textElement;
        public void RefreshAmount() => textElement.SetText($"{Coin.amount:n0}");
    }
}
