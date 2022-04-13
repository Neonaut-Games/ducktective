using Character;
using Character.Player;
using TMPro;
using UI.Uninteractable;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.Menus
{
    public class MainMenu : MonoBehaviour
    {

        private LoadingScreen _loadingScreen;
        public TextMeshProUGUI version;

        private void Start()
        {
            version.SetText(Application.version);
            _loadingScreen = FindObjectOfType<LoadingScreen>();
        }

        public void StartGame()
        {
            AudioManager.ButtonClick();
            _loadingScreen.Load("house00");
            PlayerStats.startPlayingTime = (int) Time.time;
        }
        
        public void Tutorial()
        {
            AudioManager.ButtonClick();
            _loadingScreen.Load("tutorial");
        }

        public void About()
        {
            AudioManager.ButtonClick();
            Application.OpenURL("https://coopersully.me/projects/ducktective/");
        }
    
        public void QuitGame()
        {
            AudioManager.ButtonClick();
            Application.Quit();
        }

    }
}
