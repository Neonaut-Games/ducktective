using UnityEngine;

public class PlayerLevel : MonoBehaviour
{

    private static int _questLevel;

    public static int GetLevel()
    {
        return _questLevel;
    }

    public static void SetLevel(int level)
    {
        // Don't let the player's quest level downgrade
        if (level < _questLevel) return;
        
        Debug.Log("Player's quest level was set to " + level);
        
        // Set the player's new quest level
        _questLevel = level;
    }
    
}
