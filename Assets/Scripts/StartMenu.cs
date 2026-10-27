using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public void StartGame()
    {
        // load your game scene
        SceneManager.LoadScene("Minigame"); 
    }

    public void QuitGame()
    {
        Debug.Log("Quit!"); // will show in editor
        Application.Quit();  // works in build
    }
}