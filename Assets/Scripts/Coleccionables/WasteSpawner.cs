using UnityEngine;

public class WasteSpawner : MonoBehaviour
{
    [Header("Waste Prefabs")]
    [SerializeField] private GameObject[] goodWastePrefabs;
    [SerializeField] private GameObject[] badWastePrefabs;

    [Header("Spawn Settings")]
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private int maxWasteOnScreen = 20;
    [SerializeField, Range(0f, 1f)] private float goodWasteChance = 0.7f;

    [Header("Arena Limits")]
    [SerializeField] private float horizontalLimit = 7.5f;
    [SerializeField] private float verticalLimit = 3.5f;

    private float spawnTimer;
    private int currentWasteCount = 0;

    private void Start()
    {
        ResetSpawner();
    }

    private void Update()
    {
        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0f && currentWasteCount < maxWasteOnScreen)
        {
            SpawnWaste();
            spawnTimer = spawnInterval;
        }
    }

    private void SpawnWaste()
    {
        Vector2 spawnPosition = new Vector2(
            Random.Range(-horizontalLimit, horizontalLimit),
            Random.Range(-verticalLimit, verticalLimit)
        );

        GameObject selectedPrefab = null;

        bool spawnGoodWaste = Random.value <= goodWasteChance;

        if (spawnGoodWaste)
        {
            if (goodWastePrefabs == null || goodWastePrefabs.Length == 0)
            {
                return;
            }

            int randomIndex = Random.Range(0, goodWastePrefabs.Length);

            selectedPrefab = goodWastePrefabs[randomIndex];
        }
        else
        {
            if (badWastePrefabs == null || badWastePrefabs.Length == 0)
            {
                return;
            }

            int randomIndex = Random.Range(0, badWastePrefabs.Length);

            selectedPrefab = badWastePrefabs[randomIndex];
        }

        if (selectedPrefab == null)
        {
            return;
        }

        GameObject spawnedWaste = Instantiate(
            selectedPrefab,
            spawnPosition,
            Quaternion.identity
        );

        CollectibleItem collectible =
            spawnedWaste.GetComponent<CollectibleItem>();

        if (collectible != null)
        {
            collectible.SetSpawner(this);
        }

        currentWasteCount++;
    }

    public void NotifyWasteCollected()
    {
        currentWasteCount--;

        if (currentWasteCount < 0)
        {
            currentWasteCount = 0;
        }
    }

    public void IncreaseGoodWasteChance(float amount)
    {
        goodWasteChance += amount;
        goodWasteChance = Mathf.Clamp01(goodWasteChance);
    }

    public void ResetSpawner()
    {
        currentWasteCount = 0;
        spawnTimer = spawnInterval;
        enabled = true;
    }
}