using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip impactSound_normal;
    public AudioClip hitSound_normal;
    public AudioClip playerHitSound_normal;
    
    private AudioSource impactSoundAudioSource;
    private AudioSource hitSoundAudioSource;
    private AudioSource playerHitSoundAudioSource;

    public float randomPitchAmount = 0.2f;

    private void Awake()
    {
        impactSoundAudioSource = gameObject.AddComponent<AudioSource>();
        hitSoundAudioSource = gameObject.AddComponent<AudioSource>();
        playerHitSoundAudioSource = gameObject.AddComponent<AudioSource>();
    }

    public void PlayImpactSound_Normal()
    {
        impactSoundAudioSource.pitch = RandomPitch();
        impactSoundAudioSource.PlayOneShot(impactSound_normal);
    }
    
    public void PlayHitSound_Normal()
    {
        hitSoundAudioSource.pitch = RandomPitch();
        hitSoundAudioSource.PlayOneShot(hitSound_normal);
    }
    
    public void PlayPlayerHitSound()
    {
        playerHitSoundAudioSource.pitch = RandomPitch();
        playerHitSoundAudioSource.PlayOneShot(playerHitSound_normal);
    }

    private float RandomPitch()
    {
        float newPitch = 1f - ((randomPitchAmount / 2) - Random.Range(0, randomPitchAmount));
        return newPitch;
    }
}
