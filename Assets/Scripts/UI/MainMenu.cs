using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class MainMenu : MonoBehaviour
    {

        public void StartGame()
        {
            SceneManager.LoadScene(1);
        }

        public void About()
        {
            Application.OpenURL("https://coopersully.me/projects/ducktective/");
        }
    
        public void QuitGame()
        {
            Application.Quit();
        }

    }
}
