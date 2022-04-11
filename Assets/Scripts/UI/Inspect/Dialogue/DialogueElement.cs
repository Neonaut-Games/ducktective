namespace UI.Inspect.Dialogue
{
    public class DialogueElement
    {
        private readonly string _author;
        private readonly string _message;
        private readonly VoiceType _voice;

        public DialogueElement(string author, string message, VoiceType voice)
        {
            _author = author;
            _message = message;
            _voice = voice;
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

    }
}