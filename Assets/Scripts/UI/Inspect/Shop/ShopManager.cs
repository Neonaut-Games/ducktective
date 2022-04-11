using System;
using System.Collections;
using Character;
using Character.Player;
using TMPro;
using UI.Uninteractable;
using UnityEngine;

namespace UI.Inspect.Shop
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
        private string _author;
        private string _message;
        private int _price;
        private static readonly int IsEnabled = Animator.StringToHash("isEnabled");
        private static readonly int IsOpen = Animator.StringToHash("isOpen");

        private void Update()
        {
            // If the player presses space at any time
            if (Input.GetKeyDown(KeyCode.Space)) {
                /* If the player is currently in inspection mod
                mode and there is a trigger currently loaded. */
                if (PlayerInspect.movementRestricted && PlayerInspect.loadedTrigger != null)
                {
                    // If the loaded trigger is a DialogueTrigger
                    if (PlayerInspect.loadedTrigger.GetType() == typeof(ShopTrigger)) EndShop(false);
                }
            }
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
            PlayerInspect.BeginInspect();

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
            startButton.SetBool(IsEnabled, !activity);
            shopBox.SetBool(IsOpen, activity);
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
            if (CoinsManager.Balance() < _price)
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
            CoinsManager.Withdraw(_price);
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
            PlayerInspect.EndInspect();

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
