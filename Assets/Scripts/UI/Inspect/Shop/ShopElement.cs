using UI.Inspect.Dialogue;

namespace UI.Inspect.Shop
{
    public class ShopElement
    {
        private readonly string _author;
        private readonly string _message;
        private readonly VoiceType _voice;
        private readonly int _price;

        public ShopElement(string author, string message, VoiceType voice, int price)
        {
            _author = author;
            _message = message;
            _voice = voice;
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
        
        public VoiceType GetVoice()
        {
            return _voice;
        }
        
        public int GetPrice()
        {
            return _price;
        }

    }
}