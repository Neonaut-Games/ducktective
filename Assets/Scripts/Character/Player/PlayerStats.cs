using System.Collections;
using UnityEngine;

namespace Character.Player
{
    public static class PlayerStats
    {
        private static int _playtime;

        public static int totalKills;
        public static int totalDeaths;

        public static int totalHits;
        public static int totalMisses;

        public static float GetKDR()
        {
            if (totalDeaths == 0) return totalKills;
            return (float) totalKills / totalDeaths;
        }
        
        public static float GetAccuracy()
        {
            if (totalMisses == 0) return 100;
            return (float) totalHits / (totalMisses + totalHits) * 100;
        }

        public static IEnumerator StartCounter()
        {
            _playtime++;
            yield return new WaitForSeconds(1.0f);
        }

        public static string GetTime()
        {
            var min = _playtime / 60;
            var sec = _playtime % 60;
            return min + ":" + sec;
        }

        public static int GetScore()
        {
            int score = (int) ((GetKDR() * GetAccuracy() * 100) - _playtime);
            return score;
        }
        

    }
}
