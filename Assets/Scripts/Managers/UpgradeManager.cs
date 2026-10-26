using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject upgradePanel;
    [SerializeField] private GameObject darkOverlay;

    [Header("Game References")]
    [SerializeField] private CollectionManager collectionManager;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private WasteSpawner wasteSpawner;
    [SerializeField] private ArenaResetManager arenaResetManager;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip upgradeSelectionClip;

    [Header("Upgrade Values")]
    [SerializeField] private float magnetIncrease = 1.5f;
    [SerializeField] private float goodWasteIncrease = 0.1f;
    [SerializeField] private float collectionRangeIncrease = 0.25f;

    private void Awake()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    private void Start()
    {
        if (upgradePanel != null)
        {
            upgradePanel.SetActive(false);
        }

        if (darkOverlay != null)
        {
            darkOverlay.SetActive(false);
        }
    }

    public void OpenUpgradePanel()
    {
        if (darkOverlay != null)
        {
            darkOverlay.SetActive(true);
        }

        if (upgradePanel != null)
        {
            upgradePanel.SetActive(true);
        }

        Time.timeScale = 0f;
    }

    public void SelectMagnet()
    {
        PlayUpgradeSelectionSound();

        if (playerController != null)
        {
            playerController.IncreaseMagnetRadius(magnetIncrease);
        }

        FinishUpgradeSelection();
    }

    public void SelectGoodWaste()
    {
        PlayUpgradeSelectionSound();

        if (wasteSpawner != null)
        {
            wasteSpawner.IncreaseGoodWasteChance(goodWasteIncrease);
        }

        FinishUpgradeSelection();
    }

    public void SelectCollectionRange()
    {
        PlayUpgradeSelectionSound();

        if (playerController != null)
        {
            playerController.IncreaseCollectionRadius(collectionRangeIncrease);
        }

        FinishUpgradeSelection();
    }

    private void PlayUpgradeSelectionSound()
    {
        if (audioSource != null && upgradeSelectionClip != null)
        {
            audioSource.PlayOneShot(upgradeSelectionClip);
        }
    }

    private void FinishUpgradeSelection()
    {
        if (collectionManager != null)
        {
            collectionManager.ResetCollection();
        }

        if (arenaResetManager != null)
        {
            arenaResetManager.ResetArena();
        }

        if (upgradePanel != null)
        {
            upgradePanel.SetActive(false);
        }

        if (darkOverlay != null)
        {
            darkOverlay.SetActive(false);
        }

        Time.timeScale = 1f;
    }
}