using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip mainTheme;
    public AudioClip hitSound_normal;
    public AudioClip playerHitSound_normal;
    public AudioClip coinSound_normal;

    public List<AudioClip> bounceSounds;
    
    private AudioSource mainThemeAudioSource;
    private AudioSource bounceSoundAudioSource;
    private AudioSource hitSoundAudioSource;
    private AudioSource playerHitSoundAudioSource;
    private AudioSource coinSoundAudioSource;

    public float randomPitchAmount = 0.2f;

    private void Awake()
    {
        mainThemeAudioSource = gameObject.AddComponent<AudioSource>();
        bounceSoundAudioSource = gameObject.AddComponent<AudioSource>();
        hitSoundAudioSource = gameObject.AddComponent<AudioSource>();
        playerHitSoundAudioSource = gameObject.AddComponent<AudioSource>();
        coinSoundAudioSource = gameObject.AddComponent<AudioSource>();
    }

    private void Start()
    {
        mainThemeAudioSource.clip = mainTheme;
        mainThemeAudioSource.loop = true;
        mainThemeAudioSource.Play();
    }

    public void PlayBounceSound_Normal()
    {
        bounceSoundAudioSource.pitch = RandomPitch();
        bounceSoundAudioSource.PlayOneShot(RandomBounceClip());
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

    public void PlayCoinSound()
    {
        coinSoundAudioSource.pitch = RandomPitch();
        coinSoundAudioSource.PlayOneShot(coinSound_normal);
    }

    private float RandomPitch()
    {
        float newPitch = 1f - ((randomPitchAmount / 2) - Random.Range(0, randomPitchAmount));
        return newPitch;
    }

    private AudioClip RandomBounceClip()
    {
        int randomIndex = Random.Range(0, bounceSounds.Count - 1);
        return bounceSounds[randomIndex];
    }
}
