using Character.Player;
using SceneManagement;
using TMPro;
using UnityEngine;

namespace UI
{
    public class VictoryScreen : MonoBehaviour
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
            FindObjectOfType<PlayerInspect>().BeginInspect();

            kills.SetText(PlayerStats.totalKills + " Total Kills");
            deaths.SetText(PlayerStats.totalDeaths + " Total Deaths");
            kdr.SetText(PlayerStats.GetKDR() + " KDR");
            
            hits.SetText(PlayerStats.totalHits + " Hits Landed");
            misses.SetText(PlayerStats.totalMisses + " Hits Missed");
            accuracy.SetText(PlayerStats.GetAccuracy() + "% Accuracy");

            time.SetText(PlayerStats.GetTime() + " Completion Time");
            
            score.SetText(PlayerStats.GetScore().ToString());
        }

        public void Exit()
        {
            Application.Quit();
        }
        
    }
}