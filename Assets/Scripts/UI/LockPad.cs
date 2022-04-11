using TMPro;
using UnityEngine;

namespace UI
{
    public class LockPad : MonoBehaviour
    {
        private string _currentSequence;

        public TextMeshProUGUI currentSequenceUI;
        public string correctSequence;

        public void EnterCharacter(string character)
        {
            _currentSequence += character;
            currentSequenceUI.SetText(_currentSequence);

            if (_currentSequence.Length < 4) return;
            if (_currentSequence.Equals(correctSequence)) Success();
            else Failure();
        }

        private void Success()
        {
            
        }

        private void Failure()
        {
            
        }
        
    }
}