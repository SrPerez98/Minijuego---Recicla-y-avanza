using TMPro;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private PlayerHealth playerHealth;

    private void Update()
    {
        UpdateHealthText();
    }

    private void UpdateHealthText()
    {
        if (healthText == null || playerHealth == null)
        {
            return;
        }

        healthText.text =
            "Health: " +
            playerHealth.CurrentHealth +
            " / " +
            playerHealth.MaxHealth;
    }
}