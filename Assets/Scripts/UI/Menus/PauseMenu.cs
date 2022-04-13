using System;
using System.Text;
using System.Web;
using Character;
using Character.Player;
using TMPro;
using UI.Uninteractable;
using UnityEngine;

namespace UI.Menus
{
    public class PauseMenu : MonoBehaviour
    {

        public static bool isPaused;
        public GameObject pauseMenu;
        
        [Header("Section Elements")]
        public GameObject mainMenu;
        
        public GameObject questMenu;

        public GameObject optionsMenu;
        public TextMeshProUGUI questHeader;
        public TextMeshProUGUI questDescription;
        
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
                if (LoadingScreen.isLoading)
                {
                    AudioManager.Decline();
                    return;
                }
                
                if (!pauseMenu.activeSelf) Pause();
                else Resume();
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (LoadingScreen.isLoading)
                {
                    AudioManager.Decline();
                    return;
                }

                if (!pauseMenu.activeSelf) Pause();
                RevealSectionQuest();
                
            }
            
            if (Input.GetKeyDown(KeyCode.R))
            {
                if (LoadingScreen.isLoading)
                {
                    AudioManager.Decline();
                    return;
                }

                if (!pauseMenu.activeSelf) Pause();
                RevealSectionTickets();

            }
        }

        private void Pause()
        {
            isPaused = true;
            AudioManager.Pause();
            RevealSectionMain();
            pauseMenu.SetActive(true);
            Time.timeScale = 0.0f;
        }

        public void Resume()
        {
            isPaused = false;
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
            questMenu.SetActive(false);
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
            questMenu.SetActive(false);
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

        #region Quest Info

        public void RevealSectionQuest()
        {
            RevealSection(questMenu);
            RefreshQuestInfo();
        }

        private void RefreshQuestInfo()
        {
            var cosmeticLevel = PlayerLevel.questLevel + 1;
            questHeader.SetText("Quest " + cosmeticLevel);

            string description;
            switch (PlayerLevel.questLevel)
            {
                case 0:
                    description = 
                        "Head over to the post office to pick up mom's package.";
                    break;
                case 1:
                    description = 
                        "The cryptic letter at the post office gave you a bad feeling; head back home to check on mom.";
                    break;
                case 2:
                    description = 
                        "Go to the police station to report your mom's disappearance and get assistance in your search to get her back.";
                    break;
                case 3:
                    description = 
                        "Randy suggested talking to Quackintinus about checking the town's cameras; head to the top floor of the police station to find him.";
                    break;
                case 4:
                    description = 
                        "Quackintinius needs a DNA sample from the kidnapper; search the house for any clues the kidnapper may have left behind.";
                    break;
                case 5:
                    description = 
                        "You found a black feather at the house and Quackintinus may be able to use it to locate your mother's kidnapper. " +
                        "Head back to the top floor of the police station to give him the sample..";
                    break;
                case 6:
                    description = 
                        "Travel to Meemaw's Transportation Station and try to get private transportation to Temedos Island.";
                    break;
                case 7:
                    description = 
                        "You'll need 50 coins for transportation to Temedos. " +
                        "Try rough-housing a few folks around town to see if they have any spare change for the cause.";
                    break;
                case 8:
                    description = 
                        "Travel to the castle at the top of the island and try to find your mother's kidnapper; he can't be too far.";
                    break;
                case 9:
                    description = 
                        "What are you waiting for? Unlock your mom's cell and get her out of there!";
                    break;
                default:
                    throw new ArgumentException("Quest menu not updated to fit current quest level.");
            }
            
            questDescription.SetText(description);
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
                .Append("Quest Level: " + (PlayerLevel.questLevel + 1))
                .AppendLine()
                .AppendLine("Total Kills: " + PlayerStats.totalKills)
                .AppendLine("Total Deaths: " + PlayerStats.totalDeaths)
                .AppendLine("Total K/D: " + PlayerStats.GetKdr().ToString("0.00"))
                .AppendLine()
                .AppendLine("Total Hits Landed: " + PlayerStats.totalHits)
                .AppendLine("Total Hits Missed: " + PlayerStats.totalMisses)
                .AppendLine("Total Accuracy: " + PlayerStats.GetAccuracy().ToString("0.00") + "%")
                .AppendLine()
                .AppendLine("Time Elapsed: " + PlayerStats.GetTimePlayed());

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
            describeTheBug.SetTextWithoutNotify("");
            toReproduce.SetTextWithoutNotify("");
            expectedBehavior.SetTextWithoutNotify("");
            additionalContext.SetTextWithoutNotify("");
            
            describeTheIdea.SetTextWithoutNotify("");
            problemRelation.SetTextWithoutNotify("");
            additionalContext2.SetTextWithoutNotify("");
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
