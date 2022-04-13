using System;
using UI.Inspect.Dialogue;
using UnityEngine;

namespace UI.Inspect.Shop
{
    [Serializable]
    public class Shop
    {
        public Shop(string author, string message, VoiceType voice, int price)
        {
            this.author = author;
            this.message = message;
            this.price = price;
            this.voice = voice;
        }

        [SerializeField] private string author;
        [TextArea(3, 10)] [SerializeField] private string message;
        [SerializeField] private int price;
        [SerializeField] private VoiceType voice;
        
        public ShopElement GetElement()
        {
            return new ShopElement(author, message, voice, price);
        }
    }
}
