using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [Header("Projectile")]
    [SerializeField] private float speed = 6f;
    [SerializeField] private int damage = 1;
    [SerializeField] private float lifetime = 4f;

    [Header("Audio")]
    [SerializeField] private AudioClip impactClip;
    [SerializeField] private float impactVolume = 1f;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    public void Initialize(Vector2 direction)
    {
        rb.linearVelocity = direction.normalized * speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerHealth playerHealth =
            other.GetComponentInParent<PlayerHealth>();

        if (playerHealth == null)
        {
            return;
        }

        playerHealth.TakeDamage(damage);

        if (impactClip != null)
        {
            AudioSource.PlayClipAtPoint(
                impactClip,
                transform.position,
                impactVolume
            );
        }

        Destroy(gameObject);
    }
}