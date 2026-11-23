using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float preferredDistance = 5f;
    [SerializeField] private float distanceTolerance = 0.5f;

    [Header("Attack")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float attackCooldown = 2f;

    private Transform player;
    private Rigidbody2D rb;

    private GameFeedbackManager feedbackManager;

    private float attackCooldownRemaining = 0f;

    private void Awake()
    {
        rb =
            GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        GameObject playerObject =
            GameObject.FindGameObjectWithTag(
                "Player"
            );

        if (playerObject != null)
        {
            player =
                playerObject.transform;
        }

        feedbackManager =
            FindAnyObjectByType<
                GameFeedbackManager
            >();
    }

    private void Update()
    {
        UpdateAttackCooldown();
        TryShoot();
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        if (player == null)
        {
            rb.linearVelocity =
                Vector2.zero;

            return;
        }

        Vector2 direction =
            (
                (Vector2)player.position -
                rb.position
            ).normalized;

        float distance =
            Vector2.Distance(
                rb.position,
                player.position
            );

        if (
            distance >
            preferredDistance +
            distanceTolerance
        )
        {
            rb.linearVelocity =
                direction *
                moveSpeed;
        }
        else if (
            distance <
            preferredDistance -
            distanceTolerance
        )
        {
            rb.linearVelocity =
                -direction *
                moveSpeed;
        }
        else
        {
            rb.linearVelocity =
                Vector2.zero;
        }
    }

    private void TryShoot()
    {
        if (
            player == null ||
            projectilePrefab == null ||
            firePoint == null
        )
        {
            return;
        }

        if (
            attackCooldownRemaining >
            0f
        )
        {
            return;
        }

        Shoot();

        attackCooldownRemaining =
            attackCooldown;
    }

    private void Shoot()
    {
        Vector2 direction =
            (
                (Vector2)player.position -
                (Vector2)firePoint.position
            ).normalized;

        GameObject projectileObject =
            Instantiate(
                projectilePrefab,
                firePoint.position,
                Quaternion.identity
            );

        EnemyProjectile projectile =
            projectileObject
                .GetComponent<
                    EnemyProjectile
                >();

        if (projectile != null)
        {
            projectile.Initialize(
                direction
            );
        }

        if (feedbackManager != null)
        {
            feedbackManager
                .PlayEnemyShootFeedback();
        }
    }

    private void UpdateAttackCooldown()
    {
        if (
            attackCooldownRemaining >
            0f
        )
        {
            attackCooldownRemaining -=
                Time.deltaTime;
        }
    }
}