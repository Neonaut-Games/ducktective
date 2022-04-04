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
        public GameObject optionsMenu;
        public GameObject statsMenu;
        public GameObject controlsMenu;
        public TextMeshProUGUI statsElement;

        [Header("Audio Settings")]
        public AudioSource buttonClick;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (!pauseMenu.activeSelf) Pause();
                else Resume();
            }
        }

        public void RevealSectionMain()
        {
            buttonClick.Play();
            optionsMenu.SetActive(false);
            statsMenu.SetActive(false);
            controlsMenu.SetActive(false);
            mainMenu.SetActive(true);
        }

        private void Pause()
        {
            buttonClick.Play();
            RevealSectionMain();
            pauseMenu.SetActive(true);
            Time.timeScale = 0.0f;
        }

        public void Resume()
        {
            buttonClick.Play();
            pauseMenu.SetActive(false);
            Time.timeScale = 1.0f;
        }

        public void ReportAnIssue() => Application.OpenURL("https://github.com/coopersully/ducktective/issues/new?assignees=&labels=bug&template=bug_report.md&title=");
        public void SuggestAFeature() => Application.OpenURL("https://github.com/coopersully/ducktective/issues/new?assignees=&labels=enhancement&template=feature_request.md&title=");

        public void RevealSectionStats()
        {
            buttonClick.Play();
            RefreshStatsElement();
            mainMenu.SetActive(false);
            optionsMenu.SetActive(false);
            controlsMenu.SetActive(false);
            statsMenu.SetActive(true);
        }
        
        public void RevealSectionControls()
        {
            buttonClick.Play();
            mainMenu.SetActive(false);
            optionsMenu.SetActive(false);
            statsMenu.SetActive(false);
            controlsMenu.SetActive(true);
        }
        
        public void RevealSectionOptions()
        {
            buttonClick.Play();
            mainMenu.SetActive(false);
            statsMenu.SetActive(false);
            controlsMenu.SetActive(false);
            optionsMenu.SetActive(true);
        }
        
        public void QuitGame()
        {
            buttonClick.Play();
            Application.Quit();
        }

        private void RefreshStatsElement()
        {
            var playerStats = new StringBuilder();
            playerStats
                .Append("Quest Level: " + PlayerLevel.questLevel)
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
