using UnityEngine;

namespace Character.Player
{
    public class PlayerLevel : MonoBehaviour
    {

        public static int questLevel;

        public static void SetQuestLevel(int level)
        {
            // Don't let the player's quest level downgrade
            if (level < questLevel) return;
        
            DuckLog.Normal("Player's quest level was set to " + level);
        
            // Set the player's new quest level
            questLevel = level;
        }
        
        public static bool IsQualified(int level) => questLevel >= level && questLevel <= level + 1;
    }
}
