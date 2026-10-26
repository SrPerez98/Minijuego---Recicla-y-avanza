using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Prefabs")]
    [SerializeField] private GameObject meleeEnemyPrefab;
    [SerializeField] private GameObject rangedEnemyPrefab;

    [Header("Spawn Settings")]
    [SerializeField] private float initialSpawnInterval = 3f;
    [SerializeField] private float minimumSpawnInterval = 0.8f;
    [SerializeField] private float spawnAcceleration = 0.1f;

    [Header("Enemy Progression")]
    [SerializeField] private float rangedEnemyStartTime = 20f;

    [Range(0f, 1f)]
    [SerializeField] private float rangedEnemyChance = 0.25f;

    [Header("Arena Limits")]
    [SerializeField] private float horizontalLimit = 8f;
    [SerializeField] private float verticalLimit = 4f;

    private float currentSpawnInterval;
    private float spawnTimer;
    private float gameTime;

    private void Start()
    {
        ResetSpawner();
    }

    private void Update()
    {
        gameTime += Time.deltaTime;
        HandleSpawning();
    }

    private void HandleSpawning()
    {
        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0f)
        {
            SpawnEnemy();

            currentSpawnInterval -= spawnAcceleration;

            currentSpawnInterval = Mathf.Max(
                currentSpawnInterval,
                minimumSpawnInterval
            );

            spawnTimer = currentSpawnInterval;
        }
    }

    private void SpawnEnemy()
    {
        GameObject enemyPrefab = ChooseEnemyPrefab();

        if (enemyPrefab == null)
        {
            Debug.LogWarning("Enemy prefab is not assigned.");
            return;
        }

        Vector2 spawnPosition = GetRandomSpawnPosition();

        Instantiate(
            enemyPrefab,
            spawnPosition,
            Quaternion.identity
        );
    }

    private GameObject ChooseEnemyPrefab()
    {
        if (gameTime < rangedEnemyStartTime)
        {
            return meleeEnemyPrefab;
        }

        if (Random.value <= rangedEnemyChance)
        {
            return rangedEnemyPrefab;
        }

        return meleeEnemyPrefab;
    }

    private Vector2 GetRandomSpawnPosition()
    {
        int side = Random.Range(0, 4);

        switch (side)
        {
            case 0:
                return new Vector2(
                    Random.Range(
                        -horizontalLimit,
                        horizontalLimit
                    ),
                    verticalLimit
                );

            case 1:
                return new Vector2(
                    Random.Range(
                        -horizontalLimit,
                        horizontalLimit
                    ),
                    -verticalLimit
                );

            case 2:
                return new Vector2(
                    -horizontalLimit,
                    Random.Range(
                        -verticalLimit,
                        verticalLimit
                    )
                );

            default:
                return new Vector2(
                    horizontalLimit,
                    Random.Range(
                        -verticalLimit,
                        verticalLimit
                    )
                );
        }
    }

    public void ResetSpawner()
    {
        currentSpawnInterval = initialSpawnInterval;
        spawnTimer = initialSpawnInterval;
        gameTime = 0f;

        enabled = true;

        Debug.Log("Enemy spawner reset.");
    }
}