using System;
using TMPro;
using UnityEngine;

public class CoinsManager : MonoBehaviour
{
    public TextMeshProUGUI textElement;
    public void RefreshAmount() => textElement.SetText(String.Format("{0:n0}", Items.Coin.amount));
}
