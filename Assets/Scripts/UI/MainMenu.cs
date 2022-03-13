using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void About()
    {
        Application.OpenURL("https://ducktective.coopersully.me/about/");
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }

}
