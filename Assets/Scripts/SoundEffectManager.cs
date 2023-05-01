using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectManager : MonoBehaviour
{
    public static SoundEffectManager Instance;

    [SerializeField]
    private AudioSource PollutionSource, DeathSource;
    public AudioClip[] EffectClips;

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    public void PlayPolution()
    {
        if(!PollutionSource.isPlaying)
            PollutionSource.Play();
    }

    public void StopEffect()
    {
        if (PollutionSource.isPlaying)
            PollutionSource.Stop();
    }

    public void PlayDeath()
    {
        DeathSource.Play();
    }
    public void StopDeath()
    {
        DeathSource.Stop();
    }
}
