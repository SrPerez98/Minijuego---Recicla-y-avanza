using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    public enum WasteType
    {
        Good,
        Bad
    }

    [Header("Waste Settings")]
    [SerializeField] private WasteType wasteType;
    [SerializeField] private float collectionValue = 10f;

    [Header("Lifetime")]
    [SerializeField] private float lifetime = 12f;

    [Header("Magnet Settings")]
    [SerializeField] private float magnetSpeed = 4f;

    private CollectionManager collectionManager;
    private WasteSpawner wasteSpawner;

    private PlayerController playerController;
    private Transform playerTransform;

    private GameFeedbackManager feedbackManager;

    private float lifetimeTimer;
    private bool isBeingDestroyed = false;

    private void Start()
    {
        lifetimeTimer = lifetime;

        collectionManager =
            FindAnyObjectByType<
                CollectionManager
            >();

        playerController =
            FindAnyObjectByType<
                PlayerController
            >();

        feedbackManager =
            FindAnyObjectByType<
                GameFeedbackManager
            >();

        if (playerController != null)
        {
            playerTransform =
                playerController.transform;
        }
    }

    private void Update()
    {
        HandleMagnet();
        HandleLifetime();
    }

    public void SetSpawner(
        WasteSpawner spawner
    )
    {
        wasteSpawner = spawner;
    }

    private void HandleLifetime()
    {
        if (isBeingDestroyed)
        {
            return;
        }

        lifetimeTimer -= Time.deltaTime;

        if (lifetimeTimer <= 0f)
        {
            ExpireCollectible();
        }
    }

    private void ExpireCollectible()
    {
        if (isBeingDestroyed)
        {
            return;
        }

        isBeingDestroyed = true;

        if (wasteSpawner != null)
        {
            wasteSpawner
                .NotifyWasteCollected();
        }

        Destroy(gameObject);
    }

    private void HandleMagnet()
    {
        if (wasteType != WasteType.Good)
        {
            return;
        }

        if (
            playerController == null ||
            playerTransform == null
        )
        {
            return;
        }

        float magnetRadius =
            playerController.MagnetRadius;

        if (magnetRadius <= 0f)
        {
            return;
        }

        float distance =
            Vector2.Distance(
                transform.position,
                playerTransform.position
            );

        if (distance <= magnetRadius)
        {
            transform.position =
                Vector2.MoveTowards(
                    transform.position,
                    playerTransform.position,
                    magnetSpeed *
                    Time.deltaTime
                );
        }
    }

    private void OnTriggerEnter2D(
        Collider2D other
    )
    {
        if (isBeingDestroyed)
        {
            return;
        }

        if (!other.CompareTag("Player"))
        {
            return;
        }

        if (collectionManager == null)
        {
            Debug.LogWarning(
                "CollectionManager not found."
            );

            return;
        }

        isBeingDestroyed = true;

        Vector3 effectPosition =
            transform.position;

        if (wasteType == WasteType.Good)
        {
            collectionManager
                .AddCollection(
                    collectionValue
                );

            if (feedbackManager != null)
            {
                feedbackManager
                    .PlayGoodWasteFeedback(
                        effectPosition
                    );
            }

            Debug.Log(
                "Good waste collected."
            );
        }
        else
        {
            collectionManager
                .RemoveCollection(
                    collectionValue
                );

            if (feedbackManager != null)
            {
                feedbackManager
                    .PlayBadWasteFeedback(
                        effectPosition
                    );
            }

            Debug.Log(
                "Bad waste collected."
            );
        }

        if (wasteSpawner != null)
        {
            wasteSpawner
                .NotifyWasteCollected();
        }

        Destroy(gameObject);
    }
}