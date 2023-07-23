using UnityEngine;

public class EnemyAudio
{
    private readonly AudioSource audioSource;
    private readonly AudioClip[] audioClips;

    public EnemyAudio(AudioSource audioSource, AudioClip[] audioClips)
    {
        this.audioSource = audioSource;
        this.audioClips = audioClips;
    }

    public void PlayHitClip()
    {
        audioSource.PlayOneShot(audioClips[0]);
    }

}
