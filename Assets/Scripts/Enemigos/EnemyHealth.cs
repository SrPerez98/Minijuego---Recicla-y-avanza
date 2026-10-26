using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private int maxHealth = 3;

    [Header("Visual")]
    [SerializeField] private Animator enemyAnimator;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip deathClip;

    [Header("Death")]
    [SerializeField] private float deathAnimationDuration = 0.6f;

    private int currentHealth;
    private bool isDead = false;

    public int CurrentHealth => currentHealth;
    public int MaxHealth => maxHealth;
    public bool IsDead => isDead;

    private void Awake()
    {
        currentHealth = maxHealth;

        if (enemyAnimator == null)
        {
            enemyAnimator = GetComponentInChildren<Animator>();
        }

        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    public void TakeDamage(int damage)
    {
        if (isDead)
        {
            return;
        }

        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0);

        if (currentHealth <= 0)
        {
            StartCoroutine(Die());
            return;
        }

        if (enemyAnimator != null)
        {
            enemyAnimator.SetTrigger("Hurt");
        }
    }

    private IEnumerator Die()
    {
        if (isDead)
        {
            yield break;
        }

        isDead = true;

        MeleeEnemy meleeEnemy = GetComponent<MeleeEnemy>();

        if (meleeEnemy != null)
        {
            meleeEnemy.enabled = false;
        }

        RangedEnemy rangedEnemy = GetComponent<RangedEnemy>();

        if (rangedEnemy != null)
        {
            rangedEnemy.enabled = false;
        }

        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
        }

        if (enemyAnimator != null)
        {
            enemyAnimator.ResetTrigger("Attack");
            enemyAnimator.ResetTrigger("Hurt");
            enemyAnimator.SetBool("IsMoving", false);
            enemyAnimator.SetTrigger("Death");
        }

        if (audioSource != null && deathClip != null)
        {
            audioSource.PlayOneShot(deathClip);
        }

        yield return new WaitForSeconds(deathAnimationDuration);

        Destroy(gameObject);
    }
}