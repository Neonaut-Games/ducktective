using Character.Player;
using TMPro;
using UnityEngine;

namespace UI
{
    public class Victory : MonoBehaviour
    {
        public TextMeshProUGUI score;
        
        public TextMeshProUGUI kills;
        public TextMeshProUGUI deaths;
        public TextMeshProUGUI kdr;
        
        public TextMeshProUGUI hits;
        public TextMeshProUGUI misses;
        public TextMeshProUGUI accuracy;

        public TextMeshProUGUI time;

        private void Awake()
        {
            PlayerInspect.BeginInspect();

            kills.SetText(PlayerStats.totalKills + " Total Kills");
            deaths.SetText(PlayerStats.totalDeaths + " Total Deaths");
            kdr.SetText(PlayerStats.GetKdr() + " KDR");
            
            hits.SetText(PlayerStats.totalHits + " Hits Landed");
            misses.SetText(PlayerStats.totalMisses + " Hits Missed");
            accuracy.SetText(PlayerStats.GetAccuracy().ToString("0.00") + "% Accuracy");

            time.SetText(PlayerStats.GetTimePlayed() + " Completion Time");
            
            score.SetText(PlayerStats.GetScore());
        }

        public void Exit() => Application.Quit();
    }
}