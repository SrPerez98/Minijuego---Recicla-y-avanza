using UnityEngine;

public class ArenaResetManager : MonoBehaviour
{
    [Header("Spawner References")]
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private WasteSpawner wasteSpawner;

    public void ResetArena()
    {
        ClearEnemies();
        ClearProjectiles();
        ClearWaste();
        ResetSpawners();

        Debug.Log("Arena reset completed.");
    }

    private void ClearEnemies()
    {
        MeleeEnemy[] meleeEnemies =
            FindObjectsByType<MeleeEnemy>(
                FindObjectsInactive.Exclude
            );

        foreach (MeleeEnemy enemy in meleeEnemies)
        {
            Destroy(enemy.gameObject);
        }

        RangedEnemy[] rangedEnemies =
            FindObjectsByType<RangedEnemy>(
                FindObjectsInactive.Exclude
            );

        foreach (RangedEnemy enemy in rangedEnemies)
        {
            Destroy(enemy.gameObject);
        }
    }

    private void ClearProjectiles()
    {
        EnemyProjectile[] projectiles =
            FindObjectsByType<EnemyProjectile>(
                FindObjectsInactive.Exclude
            );

        foreach (EnemyProjectile projectile in projectiles)
        {
            Destroy(projectile.gameObject);
        }
    }

    private void ClearWaste()
    {
        CollectibleItem[] wasteItems =
            FindObjectsByType<CollectibleItem>(
                FindObjectsInactive.Exclude
            );

        foreach (CollectibleItem waste in wasteItems)
        {
            Destroy(waste.gameObject);
        }
    }

    private void ResetSpawners()
    {
        if (enemySpawner != null)
        {
            enemySpawner.ResetSpawner();
        }

        if (wasteSpawner != null)
        {
            wasteSpawner.ResetSpawner();
        }
    }
}
