using System;
using UnityEngine;

namespace UI.Shop
{
    [Serializable]
    public class Shop
    {
        public Shop(string author, string message, int price)
        {
            this.author = author;
            this.message = message;
            this.price = price;
        }

        [SerializeField] private string author;
        [TextArea(3, 10)] [SerializeField] private string message;
        [SerializeField] private int price;
        
        public ShopElement GetElement()
        {
            return new ShopElement(author, message, price);
        }
    }
}
