using System.Collections;
using UnityEngine;

public class PlayerMeleeAttack : MonoBehaviour
{
    [Header("Attack Settings")]
    [SerializeField] private int attackDamage = 1;
    [SerializeField] private float attackRange = 1.5f;
    [SerializeField] private float attackCooldown = 0.8f;

    [Header("Visual References")]
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private GameObject attackVisual;
    [SerializeField] private float attackVisualDistance = 0.8f;
    [SerializeField] private float attackVisualDuration = 0.12f;

    private float attackCooldownTimer = 0f;

    private void Awake()
    {
        if (playerAnimator == null)
        {
            playerAnimator = GetComponentInChildren<Animator>();
        }

        if (attackVisual != null)
        {
            attackVisual.SetActive(false);
        }
    }

    private void Update()
    {
        if (Time.timeScale == 0f)
        {
            return;
        }

        if (attackCooldownTimer > 0f)
        {
            attackCooldownTimer -= Time.deltaTime;
            return;
        }

        EnemyHealth closestEnemy = FindClosestEnemy();

        if (closestEnemy != null)
        {
            AttackEnemy(closestEnemy);
            attackCooldownTimer = attackCooldown;
        }
    }

    private EnemyHealth FindClosestEnemy()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(
            transform.position,
            attackRange
        );

        EnemyHealth closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (Collider2D collider in colliders)
        {
            EnemyHealth enemyHealth =
                collider.GetComponentInParent<EnemyHealth>();

            if (enemyHealth == null || enemyHealth.IsDead)
            {
                continue;
            }

            float distance = Vector2.Distance(
                transform.position,
                enemyHealth.transform.position
            );

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemyHealth;
            }
        }

        return closestEnemy;
    }

    private void AttackEnemy(EnemyHealth enemy)
    {
        if (enemy == null || enemy.IsDead)
        {
            return;
        }

        if (playerAnimator != null)
        {
            playerAnimator.SetTrigger("Attack");
        }

        enemy.TakeDamage(attackDamage);

        if (attackVisual != null)
        {
            StartCoroutine(
                ShowAttackVisual(enemy.transform.position)
            );
        }
    }

    private IEnumerator ShowAttackVisual(Vector3 enemyPosition)
    {
        Vector2 direction =
            (enemyPosition - transform.position).normalized;

        attackVisual.transform.localPosition =
            direction * attackVisualDistance;

        attackVisual.SetActive(true);

        yield return new WaitForSeconds(attackVisualDuration);

        attackVisual.SetActive(false);
    }

    public void IncreaseDamage(int amount)
    {
        attackDamage += amount;
    }

    public void IncreaseAttackRange(float amount)
    {
        attackRange += amount;
    }

    public void IncreaseAttackSpeed(float amount)
    {
        attackCooldown -= amount;

        attackCooldown = Mathf.Max(
            0.1f,
            attackCooldown
        );
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(
            transform.position,
            attackRange
        );
    }
}