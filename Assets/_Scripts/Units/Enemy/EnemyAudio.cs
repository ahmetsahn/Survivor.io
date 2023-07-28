using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class EnemyAudio : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private AudioClip[] audioClips;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        AddListeners();
    }

    private void OnDisable()
    {
        RemoveListeners();
    }


    public void PlayHitClip()
    {
        audioSource.PlayOneShot(audioClips[0]);
    }

    private void AddListeners()
    {
        if (TryGetComponent(out EnemyHealth enemyHealth))
        {
            enemyHealth.OnHit += PlayHitClip;
        }
    }

    private void RemoveListeners()
    {
        if (TryGetComponent(out EnemyHealth enemyHealth))
        {
            enemyHealth.OnHit -= PlayHitClip;
        }
    }
}
