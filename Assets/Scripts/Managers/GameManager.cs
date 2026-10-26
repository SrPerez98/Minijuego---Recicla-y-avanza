using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Game Over")]
    [SerializeField] private GameObject gameOverPanel;

    [Header("Game Systems")]
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private WasteSpawner wasteSpawner;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip gameOverClip;

    [Header("Scene Names")]
    [SerializeField] private string mainMenuSceneName = "MainMenu";

    private bool isGameOver = false;

    public bool IsGameOver => isGameOver;

    private void Awake()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    private void Start()
    {
        Time.timeScale = 1f;
        isGameOver = false;

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }

        if (enemySpawner != null)
        {
            enemySpawner.enabled = true;
        }

        if (wasteSpawner != null)
        {
            wasteSpawner.enabled = true;
        }
    }

    public void GameOver()
    {
        if (isGameOver)
        {
            return;
        }

        isGameOver = true;

        StopGameSystems();

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        if (audioSource != null && gameOverClip != null)
        {
            audioSource.PlayOneShot(gameOverClip);
        }

        Time.timeScale = 0f;

        Debug.Log("Game Over.");
    }

    private void StopGameSystems()
    {
        if (enemySpawner != null)
        {
            enemySpawner.enabled = false;
        }

        if (wasteSpawner != null)
        {
            wasteSpawner.enabled = false;
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuSceneName);
    }

    public void ExitGame()
    {
        Time.timeScale = 1f;

        Debug.Log("Closing game...");
        Application.Quit();
    }
}