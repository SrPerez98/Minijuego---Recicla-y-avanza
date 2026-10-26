using UnityEngine;
using UnityEngine.UI;

public class CollectionBarUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Image collectionFill;
    [SerializeField] private CollectionManager collectionManager;

    private void Update()
    {
        UpdateCollectionBar();
    }

    private void UpdateCollectionBar()
    {
        if (collectionFill == null || collectionManager == null)
        {
            return;
        }

        float fillValue =
            collectionManager.CurrentCollection /
            collectionManager.MaxCollection;

        collectionFill.fillAmount = fillValue;
    }
}
