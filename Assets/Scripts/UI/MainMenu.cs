using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    [RequireComponent(typeof(AudioSource))]
    public class MainMenu : MonoBehaviour
    {
        public AudioSource clickSound;
        
        public void StartGame()
        {
            ButtonClick();
            SceneManager.LoadScene("house00");
        }
        
        public void Tutorial()
        {
            ButtonClick();
            SceneManager.LoadScene("tutorial");
        }

        public void About()
        {
            ButtonClick();
            Application.OpenURL("https://coopersully.me/projects/ducktective/");
        }
    
        public void QuitGame()
        {
            ButtonClick();
            Application.Quit();
        }

        private void ButtonClick()
        {
            clickSound.Play();
        }

    }
}
