using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 2.5f;

    [Header("Attack")]
    [SerializeField] private int damage = 1;
    [SerializeField] private float attackCooldown = 1f;

    [Header("Visual")]
    [SerializeField] private Animator enemyAnimator;
    [SerializeField] private SpriteRenderer enemySpriteRenderer;

    private Transform player;
    private Rigidbody2D rb;

    private float attackCooldownTimer = 0f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        if (enemyAnimator == null)
        {
            enemyAnimator = GetComponentInChildren<Animator>();
        }

        if (enemySpriteRenderer == null)
        {
            enemySpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }
    }

    private void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            player = playerObject.transform;
        }
    }

    private void Update()
    {
        if (attackCooldownTimer > 0f)
        {
            attackCooldownTimer -= Time.deltaTime;
        }

        UpdateAnimation();
    }

    private void FixedUpdate()
    {
        if (player == null)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        Vector2 direction =
            (player.position - transform.position).normalized;

        rb.linearVelocity = direction * moveSpeed;

        UpdateFacingDirection(direction);
    }

    private void UpdateAnimation()
    {
        if (enemyAnimator == null)
        {
            return;
        }

        bool isMoving =
            rb != null &&
            rb.linearVelocity.sqrMagnitude > 0.01f;

        enemyAnimator.SetBool("IsMoving", isMoving);
    }

    private void UpdateFacingDirection(Vector2 direction)
    {
        if (enemySpriteRenderer == null)
        {
            return;
        }

        if (direction.x > 0.01f)
        {
            enemySpriteRenderer.flipX = false;
        }
        else if (direction.x < -0.01f)
        {
            enemySpriteRenderer.flipX = true;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (attackCooldownTimer > 0f)
        {
            return;
        }

        PlayerHealth playerHealth =
            collision.gameObject.GetComponentInParent<PlayerHealth>();

        if (playerHealth == null)
        {
            return;
        }

        if (enemyAnimator != null)
        {
            enemyAnimator.SetTrigger("Attack");
        }

        playerHealth.TakeDamage(damage);

        attackCooldownTimer = attackCooldown;
    }
}