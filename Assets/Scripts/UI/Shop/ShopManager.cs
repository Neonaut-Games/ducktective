using System;
using System.Collections;
using Character;
using Character.Player;
using Items;
using TMPro;
using UnityEditor;
using UnityEngine;

namespace UI.Shop
{
    public class ShopManager : MonoBehaviour
    {
        [Header("Component Settings")]
        public Animator shopBox;
        public Animator startButton;
        public TextMeshProUGUI messageElement;
        public TextMeshProUGUI priceElement;
        public TextMeshProUGUI authorElement;

        private ShopTrigger _trigger;
        private PlayerInspect _playerInspect;
        private string _author;
        private string _message;
        private int _price;

        private void Start()
        {
            _playerInspect = FindObjectOfType<PlayerInspect>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space) && PlayerInspect.movementRestricted) EndShop(false);
        }
        
        /* Initiates a given shop sequence. This
        function is called only by ShopTrigger. */
        public void StartShop(Shop shop, ShopTrigger trigger)
        {
            DuckLog.Normal("A new shop is being initiated.");
            
            // Load the trigger
            if (trigger == null)
                throw new NullReferenceException("No shop was loaded, but a sequence was triggered.");
            _trigger = trigger;

            // Enable inspection mode for the player
            _playerInspect.BeginInspect();

            // Unpack the shop element
            var subpackage = shop.GetElement();
            _author = subpackage.GetAuthor();
            _price = subpackage.GetPrice();
            _message = subpackage.GetMessage();

            ShowUI(true);
            priceElement.SetText("PURCHASE ($" + _price + ")");
            authorElement.SetText(_author);
            StartCoroutine(PlayShopElement(_message));
        }

        // Toggles the inspect indicator and shop box
        private void ShowUI(bool activity)
        {
            startButton.SetBool("isEnabled", !activity);
            shopBox.SetBool("isOpen", activity);
        }
        
        /* Displays the "write-on" animation for the message along
        with the character typing sound. */
        private IEnumerator PlayShopElement(string message)
        {
            // Clear the previous message
            messageElement.SetText("");
            foreach (char character in message)
            {
                /* Animate message typing onto screen
                character-by-character with a typing sound. */
                if (!AudioManager.a.messageSound.isPlaying) AudioManager.a.messageSound.Play();
                messageElement.SetText(messageElement.text + character);

                yield return new WaitForSeconds(0.025f);
            }
        }

        public void Purchase()
        {
            // Check if the player has the required amount of coins
            if (Coin.amount < _price)
            {
                /* If the player does not have enough coins to purchase
                the given item, play a "declined" audio cue and cancel
                the purchase (this should not close the shop screen). */
                
                DuckLog.Normal("An item failed to be purchased.");
                AudioManager.Decline();
                return;
            }
            
            DuckLog.Normal("An item was successfully purchased.");
            
            AudioManager.Purchase();
            Coin.amount -= _price;
            FindObjectOfType<CoinsManager>().RefreshAmount();
            EndShop(true);
        }
        
        public void Cancel()
        {
            DuckLog.Normal("An item was not purchased.");
            EndShop(false);
        }

        private void EndShop(bool wasPurchased)
        {
            DuckLog.Normal("Shop was was ended.");
            
            // Stop playing the character typing sound
            StopAllCoroutines();

            // Disable inspection mode for the player
            _playerInspect.EndInspect();

            ShowUI(false);

            // Set gameObject to active if applicable
            if (wasPurchased)
            {
                // Set the player's quest level if applicable
                if (_trigger.shouldChangeQuestLevel) PlayerLevel.SetQuestLevel(_trigger.rewardedQuestLevel);
                
                // Enable reward game object if applicable
                if (_trigger.rewardObject != null) _trigger.rewardObject.SetActive(true);
            }
        }
        
    }
}
