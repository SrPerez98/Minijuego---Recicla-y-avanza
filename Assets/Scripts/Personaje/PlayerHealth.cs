using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private int maxHealth = 5;

    [Header("Visual")]
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private SpriteRenderer playerSpriteRenderer;
    [SerializeField] private float damageFlashDuration = 0.15f;

    [Header("Death")]
    [SerializeField] private float deathAnimationDuration = 0.7f;

    private int currentHealth;
    private bool isDead = false;

    private PlayerController playerController;
    private Rigidbody2D rb;
    private GameManager gameManager;
    private GameFeedbackManager feedbackManager;

    public int CurrentHealth => currentHealth;
    public int MaxHealth => maxHealth;
    public bool IsDead => isDead;

    private void Awake()
    {
        currentHealth = maxHealth;

        playerController = GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody2D>();

        if (playerAnimator == null)
        {
            playerAnimator = GetComponentInChildren<Animator>();
        }

        if (playerSpriteRenderer == null)
        {
            playerSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }
    }

    private void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();
        feedbackManager = FindAnyObjectByType<GameFeedbackManager>();
    }

    public void TakeDamage(int damage)
    {
        if (isDead)
        {
            return;
        }

        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0);

        if (feedbackManager != null)
        {
            feedbackManager.PlayPlayerDamageFeedback();
        }

        if (playerSpriteRenderer != null)
        {
            StartCoroutine(DamageFlash());
        }

        if (currentHealth <= 0)
        {
            StartCoroutine(Die());
        }
    }

    private IEnumerator DamageFlash()
    {
        Color originalColor = playerSpriteRenderer.color;

        playerSpriteRenderer.color = Color.red;

        yield return new WaitForSeconds(damageFlashDuration);

        if (!isDead)
        {
            playerSpriteRenderer.color = originalColor;
        }
    }

    private IEnumerator Die()
    {
        if (isDead)
        {
            yield break;
        }

        isDead = true;

        if (playerController != null)
        {
            playerController.enabled = false;
        }

        PlayerMeleeAttack meleeAttack = GetComponent<PlayerMeleeAttack>();

        if (meleeAttack != null)
        {
            meleeAttack.enabled = false;
        }

        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
        }

        if (playerAnimator != null)
        {
            playerAnimator.ResetTrigger("Attack");
            playerAnimator.ResetTrigger("Dash");
            playerAnimator.SetBool("IsMoving", false);
            playerAnimator.SetTrigger("Death");
        }

        if (feedbackManager != null)
        {
            feedbackManager.PlayPlayerDeathFeedback(transform.position);
        }

        yield return new WaitForSeconds(deathAnimationDuration);

        if (gameManager != null)
        {
            gameManager.GameOver();
        }
    }
}