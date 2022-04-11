using Items;
using TMPro;
using UnityEngine;

namespace UI.Uninteractable
{
    public class CoinsManager : MonoBehaviour
    {
        private static CoinsManager _instance;
        private static int _amount;

        [Header("UI Settings")]
        public TextMeshProUGUI textElement;

        private void Start() => _instance = this;

        public static void SetBalance(int balance)
        {
            _amount = balance;
            RefreshBalance();
        }
        
        public static void Deposit(int amount)
        {
            _amount += amount;
            RefreshBalance();
        }
        
        public static void Withdraw(int amount)
        {
            _amount -= amount;
            RefreshBalance();
        }

        public static int Balance() => _amount;
        public static void RefreshBalance() => _instance.textElement.SetText($"{_amount:n0}");
    }
}
