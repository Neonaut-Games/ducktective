namespace UI.Dialogue
{
    public class DialogueElement
    {
        private string _author;
        private string _message;
        private VoiceType _voice;

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

        public void SetAuthor(string author) => _author = author;
        public void SetMessage(string message) => _message = message;
        public void SetVoice(VoiceType voice) => _voice = voice;

    }
}