using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip impactSound_normal;
    public AudioClip hitSound_normal;
    public AudioClip playerHitSound_normal;
    
    private AudioSource impactSoundAudioSource;
    private AudioSource hitSoundAudioSource;
    private AudioSource playerHitSoundAudioSource;

    private void Awake()
    {
        impactSoundAudioSource = gameObject.AddComponent<AudioSource>();
        hitSoundAudioSource = gameObject.AddComponent<AudioSource>();
        playerHitSoundAudioSource = gameObject.AddComponent<AudioSource>();
    }

    public void PlayImpactSound_Normal()
    {
        impactSoundAudioSource.PlayOneShot(impactSound_normal);
    }
    
    public void PlayHitSound_Normal()
    {
        hitSoundAudioSource.PlayOneShot(hitSound_normal);
    }
    
    public void PlayPlayerHitSound()
    {
        playerHitSoundAudioSource.PlayOneShot(playerHitSound_normal);
    }
}
