using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectManager : MonoBehaviour
{
    public static SoundEffectManager Instance;

    [SerializeField]
    private AudioSource EffectSource;
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

    public void PlayEffect(int Clip)
    {
        EffectSource.PlayOneShot(EffectClips[Clip]);
    }
}
