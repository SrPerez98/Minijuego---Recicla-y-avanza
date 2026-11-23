using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Game Over")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject gameOverDarkOverlay;

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
        // Make sure the game always starts normally.
        Time.timeScale = 1f;

        isGameOver = false;

        // Hide the Game Over interface at the beginning.
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }

        // Hide the dark overlay at the beginning.
        if (gameOverDarkOverlay != null)
        {
            gameOverDarkOverlay.SetActive(false);
        }

        // Enable gameplay systems.
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

        // Stop systems that generate new objects.
        StopGameSystems();

        // Show the dark background first.
        if (gameOverDarkOverlay != null)
        {
            gameOverDarkOverlay.SetActive(true);
        }

        // Show the Game Over panel over the dark background.
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        // Play the Game Over sound.
        if (audioSource != null && gameOverClip != null)
        {
            audioSource.PlayOneShot(gameOverClip);
        }

        // Completely pause gameplay.
        Time.timeScale = 0f;

        Debug.Log("Game Over - Game paused.");
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
        // Resume time before reloading the scene.
        Time.timeScale = 1f;

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }

    public void ReturnToMainMenu()
    {
        // Resume time before changing scenes.
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