using Character;
using Character.Player;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.Menus
{
    public class MainMenu : MonoBehaviour
    {

        public TextMeshProUGUI version;

        private void Start() => version.SetText(Application.version);

        public void StartGame()
        {
            AudioManager.ButtonClick();
            SceneManager.LoadScene("house00");
            PlayerStats.startPlayingTime = (int) Time.time;
        }
        
        public void Tutorial()
        {
            AudioManager.ButtonClick();
            SceneManager.LoadScene("tutorial");
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
