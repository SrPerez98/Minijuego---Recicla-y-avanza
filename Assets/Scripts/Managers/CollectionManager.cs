using UnityEngine;

public class CollectionManager : MonoBehaviour
{
    [Header("Collection Settings")]
    [SerializeField] private float maxCollection = 100f;
    [SerializeField] private float currentCollection = 0f;

    [Header("References")]
    [SerializeField] private UpgradeManager upgradeManager;

    private GameFeedbackManager feedbackManager;
    private PlayerController playerController;

    private bool collectionCompleted = false;

    public float CurrentCollection =>
        currentCollection;

    public float MaxCollection =>
        maxCollection;

    private void Start()
    {
        feedbackManager =
            FindAnyObjectByType<
                GameFeedbackManager
            >();

        playerController =
            FindAnyObjectByType<
                PlayerController
            >();
    }

    public void AddCollection(
        float amount
    )
    {
        if (collectionCompleted)
        {
            return;
        }

        currentCollection += amount;

        currentCollection =
            Mathf.Clamp(
                currentCollection,
                0f,
                maxCollection
            );

        Debug.Log(
            "Collection progress: " +
            currentCollection
        );

        CheckCollectionComplete();
    }

    public void RemoveCollection(
        float amount
    )
    {
        if (collectionCompleted)
        {
            return;
        }

        currentCollection -= amount;

        currentCollection =
            Mathf.Clamp(
                currentCollection,
                0f,
                maxCollection
            );

        Debug.Log(
            "Collection progress: " +
            currentCollection
        );
    }

    private void CheckCollectionComplete()
    {
        if (
            currentCollection <
            maxCollection
        )
        {
            return;
        }

        collectionCompleted = true;

        Debug.Log(
            "Collection complete!"
        );

        if (feedbackManager != null)
        {
            Vector3 position =
                transform.position;

            if (playerController != null)
            {
                position =
                    playerController
                        .transform.position;
            }

            feedbackManager
                .PlayCollectionCompleteFeedback(
                    position
                );
        }

        if (upgradeManager != null)
        {
            upgradeManager
                .OpenUpgradePanel();
        }
    }

    public void ResetCollection()
    {
        currentCollection = 0f;
        collectionCompleted = false;

        Debug.Log(
            "Collection reset."
        );
    }
}