using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip mainTheme;
    public AudioClip hitSound_normal;
    public AudioClip playerHitSound_normal;
    public AudioClip coinSound_normal;
    public AudioClip buySound;
    public AudioClip damp_transition_01;
    public AudioClip damp_transition_02;

    public List<AudioClip> bounceSounds;

    private AudioSource mainThemeAudioSource;
    private AudioSource bounceSoundAudioSource;
    private AudioSource hitSoundAudioSource;
    private AudioSource playerHitSoundAudioSource;
    private AudioSource coinSoundAudioSource;
    private AudioSource buySoundAudioSource;

    public float randomPitchAmount = 0.2f;

    public Camera mainCamera;

    private void Awake()
    {
        mainThemeAudioSource = gameObject.AddComponent<AudioSource>();
        bounceSoundAudioSource = gameObject.AddComponent<AudioSource>();
        hitSoundAudioSource = gameObject.AddComponent<AudioSource>();
        playerHitSoundAudioSource = gameObject.AddComponent<AudioSource>();
        coinSoundAudioSource = gameObject.AddComponent<AudioSource>();
        buySoundAudioSource = gameObject.AddComponent<AudioSource>();
    }

    private void Start()
    {
        mainThemeAudioSource.clip = mainTheme;
        mainThemeAudioSource.loop = true;
        mainThemeAudioSource.volume = 0.3f;
        mainThemeAudioSource.Play();
    }

    public void PlayBounceSound_Normal()
    {
        bounceSoundAudioSource.pitch = RandomPitch();
        buySoundAudioSource.volume = 1f;
        bounceSoundAudioSource.PlayOneShot(RandomBounceClip());
    }

    public void PlayHitSound_Normal()
    {
        hitSoundAudioSource.pitch = RandomPitch();
        hitSoundAudioSource.PlayOneShot(hitSound_normal);
        CameraShake();
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

    public void PlayBuySound()
    {
        buySoundAudioSource.volume = 0.35f;
        buySoundAudioSource.PlayOneShot(buySound);
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

    private void CameraShake()
    {
        mainCamera.gameObject.AddComponent<CameraShake>();
    }

    public void DampSound()
    {
        AudioHighPassFilter soundDamper = mainCamera.gameObject.AddComponent<AudioHighPassFilter>();
        soundDamper.cutoffFrequency = 2000;
        soundDamper.highpassResonanceQ = 1;

        // Transition Sound
        buySoundAudioSource.PlayOneShot(damp_transition_01);

        // Lower MainTheme Volume
        mainThemeAudioSource.volume /= 2f;
    }

    public void NormalizeSound()
    {
        Destroy(mainCamera.gameObject.GetComponent<AudioHighPassFilter>());

        // Transition Sound
        buySoundAudioSource.volume = 0.5f;
        buySoundAudioSource.PlayOneShot(damp_transition_02);

        // Lift MainTheme Volume
        mainThemeAudioSource.volume *= 2f;
    }
}