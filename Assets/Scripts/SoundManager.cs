using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip impactSound_normal;
    public AudioClip hitSound_normal;
    
    private AudioSource impactSoundAudioSource;
    private AudioSource hitSoundAudioSource;

    private void Awake()
    {
        impactSoundAudioSource = gameObject.AddComponent<AudioSource>();
        hitSoundAudioSource = gameObject.AddComponent<AudioSource>();
    }

    public void PlayImpactSound_Normal()
    {
        impactSoundAudioSource.PlayOneShot(impactSound_normal);
    }
    
    public void PlayHitSound_Normal()
    {
        hitSoundAudioSource.PlayOneShot(hitSound_normal);
    }
}
