using System.Text;
using Character.Player;
using TMPro;
using UnityEngine;

namespace UI
{
    public class PauseMenu : MonoBehaviour
    {
        
        [Header("UI Elements")]
        public GameObject pauseMenu;
        public GameObject mainMenu;
        public GameObject statsMenu;
        public TextMeshProUGUI statsElement;

        [Header("Audio Settings")]
        public AudioSource buttonClick;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                buttonClick.Play();
                if (!pauseMenu.activeSelf) Pause();
                else Resume();
            }
        }

        public void RevealMain()
        {
            buttonClick.Play();
            mainMenu.SetActive(true);
            statsMenu.SetActive(false);
        }

        public void Pause()
        {
            RevealMain();
            pauseMenu.SetActive(true);
            Time.timeScale = 0.0f;
        }

        public void Resume()
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1.0f;
        }

        public void Stats()
        {
            buttonClick.Play();
            RevealStats();
            mainMenu.SetActive(false);
            statsMenu.SetActive(true);
        }
        
        public void QuitGame()
        {
            buttonClick.Play();
            Application.Quit();
        }

        public void RevealStats()
        {
            var playerStats = new StringBuilder();
            playerStats
                .Append("Quest Level: " + PlayerLevel.GetLevel())
                .AppendLine()
                .AppendLine("Total Kills: " + PlayerStats.totalKills)
                .AppendLine("Total Deaths: " + PlayerStats.totalDeaths)
                .AppendLine("Total K/D: " + PlayerStats.GetKDR().ToString("0.00"))
                .AppendLine()
                .AppendLine("Total Hits Landed: " + PlayerStats.totalHits)
                .AppendLine("Total Hits Missed: " + PlayerStats.totalMisses)
                .AppendLine("Total Accuracy: " + PlayerStats.GetAccuracy().ToString("0.00") + "%");

            statsElement.SetText(playerStats);
        }
        
    }
}
