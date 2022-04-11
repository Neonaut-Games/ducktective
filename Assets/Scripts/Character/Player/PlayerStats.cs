using System.Collections;
using UI.Uninteractable;
using UnityEngine;

namespace Character.Player
{
    public static class PlayerStats
    {
        public static int startPlayingTime;
        
        public static int totalKills;
        public static int totalDeaths;

        public static int totalHits;
        public static int totalMisses;

        public static float GetKdr()
        {
            if (totalDeaths == 0) return totalKills;
            return (float) totalKills / totalDeaths;
        }
        
        public static float GetAccuracy()
        {
            if (totalMisses == 0) return 100;
            return (float) totalHits / (totalMisses + totalHits) * 100;
        }

        public static string GetTimePlayed() => GetMinutesPlayed() + ":" + GetSecondsPlayed().ToString("D2");
        private static int GetSecondsPlayed() => ((int) Time.time - startPlayingTime) % 60;
        private static int GetMinutesPlayed() => ((int) Time.time - startPlayingTime) / 60;


        public static string GetScore()
        {
            var score = (int) (GetKdr() * GetAccuracy() * 100 - (GetMinutesPlayed() * 5) - GetSecondsPlayed()) + CoinsManager.Balance() / 2;
            return $"{score:n0}";
        }
        

    }
}
