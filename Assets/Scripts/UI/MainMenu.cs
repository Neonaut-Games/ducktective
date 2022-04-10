using Character;
using Character.Player;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    [RequireComponent(typeof(AudioSource))]
    public class MainMenu : MonoBehaviour
    {
        public void StartGame()
        {
            AudioManager.ButtonClick();
            SceneManager.LoadScene("house00");
            StartCoroutine(PlayerStats.StartCounter());
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
