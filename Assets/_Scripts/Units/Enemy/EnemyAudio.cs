using UnityEngine;

public class EnemyAudio : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip hitClip;

    private void Start()
    {
        if (TryGetComponent(out EnemyHealth enemyHealth))
        {
            enemyHealth.OnHit += PlayHitClip;
        }
    }


    public void PlayHitClip()
    {
        audioSource.PlayOneShot(hitClip);
    }

}
