namespace UI.Shop
{
    public class ShopElement
    {
        private readonly string _author;
        private readonly string _message;
        private readonly int _price;

        public ShopElement(string author, string message, int price)
        {
            _author = author;
            _message = message;
            _price = price;
        }

        public string GetAuthor()
        {
            return _author;
        }

        public string GetMessage()
        {
            return _message;
        }
        
        public int GetPrice()
        {
            return _price;
        }

    }
}