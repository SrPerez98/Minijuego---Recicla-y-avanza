using UnityEngine;

public class GameFeedbackManager : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField] private AudioSource audioSource;

    [Header("Collectibles")]
    [SerializeField] private AudioClip goodWasteClip;
    [SerializeField] private AudioClip badWasteClip;
    [SerializeField] private AudioClip collectionCompleteClip;

    [Header("Player")]
    [SerializeField] private AudioClip playerDamageClip;
    [SerializeField] private AudioClip playerDeathClip;
    [SerializeField] private AudioClip dashClip;
    [SerializeField] private AudioClip jumpClip;

    [Header("Enemies")]
    [SerializeField] private AudioClip enemyShootClip;

    [Header("Optional VFX")]
    [SerializeField] private GameObject goodWasteVFX;
    [SerializeField] private GameObject badWasteVFX;
    [SerializeField] private GameObject dashVFX;
    [SerializeField] private GameObject deathVFX;
    [SerializeField] private GameObject collectionCompleteVFX;

    private void Awake()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    private void PlayClip(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    private void SpawnVFX(GameObject vfxPrefab, Vector3 position)
    {
        if (vfxPrefab != null)
        {
            Instantiate(vfxPrefab, position, Quaternion.identity);
        }
    }

    public void PlayGoodWasteFeedback(Vector3 position)
    {
        PlayClip(goodWasteClip);
        SpawnVFX(goodWasteVFX, position);
    }

    public void PlayBadWasteFeedback(Vector3 position)
    {
        PlayClip(badWasteClip);
        SpawnVFX(badWasteVFX, position);
    }

    public void PlayCollectionCompleteFeedback(Vector3 position)
    {
        PlayClip(collectionCompleteClip);
        SpawnVFX(collectionCompleteVFX, position);
    }

    public void PlayPlayerDamageFeedback()
    {
        PlayClip(playerDamageClip);
    }

    public void PlayPlayerDeathFeedback(Vector3 position)
    {
        PlayClip(playerDeathClip);
        SpawnVFX(deathVFX, position);
    }

    public void PlayDashFeedback(Vector3 position)
    {
        PlayClip(dashClip);
        SpawnVFX(dashVFX, position);
    }

    public void PlayJumpFeedback()
    {
        PlayClip(jumpClip);
    }

    public void PlayEnemyShootFeedback()
    {
        PlayClip(enemyShootClip);
    }
}