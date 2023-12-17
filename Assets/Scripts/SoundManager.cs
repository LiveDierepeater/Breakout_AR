using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip impactSound_normal;
    
    private AudioSource impactSoundAudioSource;

    private void Awake()
    {
        impactSoundAudioSource = gameObject.AddComponent<AudioSource>();
    }

    public void PlayImpactSound_Normal()
    {
        impactSoundAudioSource.PlayOneShot(impactSound_normal);
    }
}
