namespace Character.Player
{
    public class PlayerStats
    {
        public static bool itemFeathers;
        public static int itemCoins;

        public static int totalKills;
        public static int totalDeaths;

        public static int totalHits;
        public static int totalMisses;
            
        public static int totalConversations;

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
        
    }
}
