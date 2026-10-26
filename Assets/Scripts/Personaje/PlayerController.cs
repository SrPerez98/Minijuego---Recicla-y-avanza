using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;

    [Header("Dash")]
    [SerializeField] private float dashSpeed = 12f;
    [SerializeField] private float dashDuration = 0.15f;
    [SerializeField] private float dashCooldown = 1.5f;

    [Header("Visual Jump")]
    [SerializeField] private Transform playerVisual;
    [SerializeField] private float jumpHeight = 0.35f;
    [SerializeField] private float jumpDuration = 0.35f;
    [SerializeField] private float jumpCooldown = 0.6f;

    [Header("Collection")]
    [SerializeField] private float collectionRadius = 0.6f;
    [SerializeField] private float magnetRadius = 0f;

    [Header("Audio")]
    [SerializeField] private AudioSource walkAudioSource;

    private Rigidbody2D rb;
    private CircleCollider2D collectionCollider;

    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private GameFeedbackManager feedbackManager;

    private Vector2 movement;
    private Vector3 visualStartPosition;

    private bool isDashing = false;
    private bool isJumping = false;

    private float dashCooldownTimer = 0f;
    private float jumpCooldownTimer = 0f;

    public float DashCooldownNormalized =>
        dashCooldown <= 0f
            ? 0f
            : Mathf.Clamp01(dashCooldownTimer / dashCooldown);

    public float CollectionRadius => collectionRadius;
    public float MagnetRadius => magnetRadius;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        collectionCollider = GetComponent<CircleCollider2D>();

        if (collectionCollider != null)
        {
            collectionCollider.radius = collectionRadius;
        }

        if (playerVisual != null)
        {
            visualStartPosition = playerVisual.localPosition;
            animator = playerVisual.GetComponent<Animator>();
            spriteRenderer = playerVisual.GetComponent<SpriteRenderer>();
        }
        else
        {
            animator = GetComponentInChildren<Animator>();
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }

        if (walkAudioSource == null)
        {
            walkAudioSource = GetComponent<AudioSource>();
        }
    }

    private void Start()
    {
        feedbackManager = FindAnyObjectByType<GameFeedbackManager>();
    }

    private void Update()
    {
        if (Time.timeScale == 0f)
        {
            StopWalkSound();
            return;
        }

        UpdateCooldowns();

        if (!isDashing)
        {
            ReadMovement();
        }

        UpdateAnimation();
        UpdateWalkSound();

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            TryDash();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            TryJump();
        }
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }

        rb.linearVelocity = movement * moveSpeed;
    }

    private void ReadMovement()
    {
        movement = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
        ).normalized;

        UpdateFacingDirection();
    }

    private void UpdateAnimation()
    {
        if (animator == null)
        {
            return;
        }

        bool isMoving = movement.sqrMagnitude > 0.01f;
        animator.SetBool("IsMoving", isMoving);
    }

    private void UpdateFacingDirection()
    {
        if (spriteRenderer == null)
        {
            return;
        }

        if (movement.x > 0.01f)
        {
            spriteRenderer.flipX = false;
        }
        else if (movement.x < -0.01f)
        {
            spriteRenderer.flipX = true;
        }
    }

    private void UpdateWalkSound()
    {
        if (walkAudioSource == null)
        {
            return;
        }

        bool shouldPlay =
            movement.sqrMagnitude > 0.01f &&
            !isDashing &&
            !isJumping;

        if (shouldPlay)
        {
            if (!walkAudioSource.isPlaying)
            {
                walkAudioSource.Play();
            }
        }
        else
        {
            StopWalkSound();
        }
    }

    private void StopWalkSound()
    {
        if (walkAudioSource != null && walkAudioSource.isPlaying)
        {
            walkAudioSource.Stop();
        }
    }

    private void UpdateCooldowns()
    {
        if (dashCooldownTimer > 0f)
        {
            dashCooldownTimer -= Time.deltaTime;
        }

        if (jumpCooldownTimer > 0f)
        {
            jumpCooldownTimer -= Time.deltaTime;
        }
    }

    private void TryDash()
    {
        if (isDashing || dashCooldownTimer > 0f)
        {
            return;
        }

        if (movement.sqrMagnitude <= 0.01f)
        {
            return;
        }

        StartCoroutine(DashCoroutine());
    }

    private IEnumerator DashCoroutine()
    {
        isDashing = true;
        dashCooldownTimer = dashCooldown;

        StopWalkSound();

        if (animator != null)
        {
            animator.SetTrigger("Dash");
        }

        feedbackManager?.PlayDashFeedback(transform.position);

        Vector2 dashDirection = movement.normalized;

        rb.linearVelocity = dashDirection * dashSpeed;

        yield return new WaitForSeconds(dashDuration);

        rb.linearVelocity = Vector2.zero;

        isDashing = false;
    }

    private void TryJump()
    {
        if (isJumping || jumpCooldownTimer > 0f)
        {
            return;
        }

        if (playerVisual == null)
        {
            return;
        }

        StartCoroutine(JumpCoroutine());
    }

    private IEnumerator JumpCoroutine()
    {
        isJumping = true;
        jumpCooldownTimer = jumpCooldown;

        StopWalkSound();

        if (animator != null)
        {
            animator.SetTrigger("Jump");
        }

        if (feedbackManager != null)
        {
            feedbackManager.PlayJumpFeedback();
        }

        float elapsedTime = 0f;

        while (elapsedTime < jumpDuration)
        {
            elapsedTime += Time.deltaTime;

            float normalizedTime =
                Mathf.Clamp01(elapsedTime / jumpDuration);

            float verticalOffset =
                Mathf.Sin(normalizedTime * Mathf.PI) * jumpHeight;

            playerVisual.localPosition =
                visualStartPosition + Vector3.up * verticalOffset;

            yield return null;
        }

        playerVisual.localPosition = visualStartPosition;

        isJumping = false;
    }

    public void IncreaseCollectionRadius(float amount)
    {
        collectionRadius += amount;

        if (collectionCollider != null)
        {
            collectionCollider.radius = collectionRadius;
        }
    }

    public void IncreaseMagnetRadius(float amount)
    {
        magnetRadius += amount;
    }
}