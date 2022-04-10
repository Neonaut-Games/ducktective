using System;
using System.Text;
using System.Web;
using Character;
using Character.Player;
using TMPro;
using UnityEngine;

namespace UI
{
    public class PauseMenu : MonoBehaviour
    {
        
        public GameObject pauseMenu;
        
        [Header("Section Elements")]
        public GameObject mainMenu;
        
        public GameObject optionsMenu;
        
        public GameObject statsMenu;
        public TextMeshProUGUI statsElement;
        
        public GameObject controlsMenu;
        
        [Header("Ticket Center")]
        public GameObject ticketMenu;
        public TMP_Dropdown dropdownMenu;
        public GameObject success;
        
        [Header("Bug Report Menu")]
        public GameObject bugReportMenu;
        
        public TMP_InputField describeTheBug;
        public TMP_InputField toReproduce;
        public TMP_InputField expectedBehavior;
        public TMP_InputField additionalContext;

        [Header("Feature Suggestion Menu")]
        public GameObject featureSuggestMenu;

        public TMP_InputField describeTheIdea;
        public TMP_InputField problemRelation;
        public TMP_InputField additionalContext2;

        #region Global Settings

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (!pauseMenu.activeSelf) Pause();
                else Resume();
            }
        }

        private void Pause()
        {
            AudioManager.Pause();
            RevealSectionMain();
            pauseMenu.SetActive(true);
            Time.timeScale = 0.0f;
        }

        public void Resume()
        {
            AudioManager.Pause();
            pauseMenu.SetActive(false);
            Time.timeScale = 1.0f;
        }

        private void RevealSection(GameObject section)
        {
            // Play audio cue
            AudioManager.ButtonClick();
            
            // Set all menu sections to not active
            mainMenu.SetActive(false);
            optionsMenu.SetActive(false);
            controlsMenu.SetActive(false);
            statsMenu.SetActive(false);
            ticketMenu.SetActive(false);
            bugReportMenu.SetActive(false);
            featureSuggestMenu.SetActive(false);
            success.SetActive(false);

            // Set section to active
            section.SetActive(true);
        }
        
        private void RevealSections(GameObject[] sections)
        {
            // Play audio cue
            AudioManager.ButtonClick();
            
            // Set all menu sections to not active
            mainMenu.SetActive(false);
            optionsMenu.SetActive(false);
            controlsMenu.SetActive(false);
            statsMenu.SetActive(false);
            ticketMenu.SetActive(false);
            bugReportMenu.SetActive(false);
            featureSuggestMenu.SetActive(false);

            // Set section to active
            foreach (var section in sections)
            {
                section.SetActive(true);
            }
        }

        #endregion

        #region Main

        public void RevealSectionMain()
        {
            RevealSection(mainMenu);
        }
        
        public void QuitGame()
        {
            AudioManager.ButtonClick();
            Application.Quit();
        }
        
        #endregion

        #region Options

        public void RevealSectionOptions()
        {
            RevealSection(optionsMenu);
        }
        public void ReportAnIssue()
        {
            Application.OpenURL(
                "https://github.com/coopersully/ducktective/issues/new?assignees=&labels=bug&template=bug_report.md&title=");
        }

        public void SuggestAFeature()
        {
            Application.OpenURL(
                "https://github.com/coopersully/ducktective/issues/new?assignees=&labels=enhancement&template=feature_request.md&title=");
        }

        #endregion

        #region Stats

        public void RevealSectionStats()
        {
            RevealSection(statsMenu);
            RefreshStatsElement();
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
        
        #endregion

        #region Controls

        public void RevealSectionControls()
        {
            RevealSection(controlsMenu);
        }

        #endregion

        #region Submit a Ticket

        public void RevealSectionTickets()
        {
            RevealSection(ticketMenu);
            RevealTicketTemplate();
        }

        public void RevealTicketTemplate()
        {
            if (dropdownMenu.value == 0) RevealSections(new[] {ticketMenu, bugReportMenu}); 
            else if (dropdownMenu.value == 1) RevealSections(new[] {ticketMenu, featureSuggestMenu});
        }

        public void SubmitBugReport()
        {
            var description = describeTheBug.text;
            var reproduction = toReproduce.text;
            var behavior = expectedBehavior.text;
            var comments = additionalContext.text;
            
            var urlAdditions = 
                    "**Describe the bug.**\n"
                    + description
                    + "\n\n**To Reproduce**\n"
                    + reproduction
                    + "\n\n**Expected behavior**\n"
                    + behavior
                    + "\n\n**Computer & Game Specifications**\n"
                    + "- Operating System: " + Environment.OSVersion.VersionString
                    + "\n- Game Version: " + Application.version
                    + "\n\n**Additional context**\n"
                    + comments
               ;

            var encodedURL =
                "https://github.com/coopersully/ducktective/issues/new?assignees=&labels=bug%2Cin-game&template=bug_report.md&body="
                + HttpUtility.UrlEncode(urlAdditions);
            
            Application.OpenURL(encodedURL);
            RevealSections(new[] {ticketMenu, success}); 
        }
        
        public void SubmitFeatureRequest()
        {
            var description = describeTheIdea.text;
            var problem = problemRelation.text;
            var comments = additionalContext2.text;
            
            var urlAdditions = 
                    "**Describe your idea.**\n"
                    + description
                    + "\n\n**Is your feature request related to a problem? Please describe.**\n"
                    + problem
                    + "\n\n**Computer & Game Specifications**\n"
                    + "- Operating System: " + Environment.OSVersion.VersionString
                    + "\n- Game Version: " + Application.version
                    + "\n\n**Additional context**\n"
                    + comments
                ;

            var encodedURL =
                "https://github.com/coopersully/ducktective/issues/new?assignees=&labels=enhancement%2Cin-game&template=feature_request.md&body="
                + HttpUtility.UrlEncode(urlAdditions);
            
            Application.OpenURL(encodedURL);
            RevealSections(new[] {ticketMenu, success});
        }

        #endregion

    }
}
