using TMPro;
using UnityEngine;

namespace Character.Player
{
    public class PlayerLevel : MonoBehaviour
    {

        private static int _questLevel;
        public TextMeshProUGUI levelIndicator;

        public static int GetLevel()
        {
            return _questLevel;
        }

        public static void SetLevel(int level)
        {
            // Don't let the player's quest level downgrade
            if (level < _questLevel) return;
        
            DuckLog.Normal("Player's quest level was set to " + level);
        
            // Set the player's new quest level
            _questLevel = level;
            FindObjectOfType<PlayerLevel>().levelIndicator.SetText(level.ToString());
        }

        public static bool IsQualified(int level)
        {
            return _questLevel >= level && _questLevel <= level + 1;
        }
    
    }
}
