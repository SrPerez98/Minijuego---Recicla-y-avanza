using UnityEngine;
using UnityEngine.UI;

public class HealthHeartsUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private Image[] hearts;

    private void Update()
    {
        UpdateHearts();
    }

    private void UpdateHearts()
    {
        if (playerHealth == null || hearts == null)
        {
            return;
        }

        for (int i = 0; i < hearts.Length; i++)
        {
            if (hearts[i] == null)
            {
                continue;
            }

            hearts[i].enabled =
                i < playerHealth.CurrentHealth;
        }
    }
}
