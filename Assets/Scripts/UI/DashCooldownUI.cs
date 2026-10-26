using UnityEngine;
using UnityEngine.UI;

public class DashCooldownUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Image dashCooldownFill;
    [SerializeField] private PlayerController playerController;

    private void Update()
    {
        UpdateDashCooldown();
    }

    private void UpdateDashCooldown()
    {
        if (dashCooldownFill == null ||
            playerController == null)
        {
            return;
        }

        dashCooldownFill.fillAmount =
            playerController.DashCooldownNormalized;
    }
}
