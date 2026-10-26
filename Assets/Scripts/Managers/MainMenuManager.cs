using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("MainArena");
    }

    public void OpenSettings()
    {
        Debug.Log("Settings menu not implemented yet.");
    }

    public void ExitGame()
    {
        Debug.Log("Game closed.");

        Application.Quit();
    }
}