using UnityEngine;

namespace UI
{
    public class PauseMenu : MonoBehaviour
    {
        
        public GameObject pauseMenu;

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
            pauseMenu.SetActive(true);
            Time.timeScale = 0.0f;
        }

        private void Resume()
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1.0f;
        }
    
        private void QuitGame()
        {
            Application.Quit();
        }
        
    }
}
