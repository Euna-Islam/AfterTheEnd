using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource BackgroundMusic1;
    public AudioSource BackgroundMusic2, BackgroundMusic3, BackgroundMusic4;

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
        PlayBackgroundMusic();
    }


    public void PlayBackgroundMusic()
    {
        BackgroundMusic1.Play();
        BackgroundMusic2.Play();
        BackgroundMusic3.Play();
        BackgroundMusic4.Play();
    }

    public void PlayBackgroundMusic2()
    {
        StartCoroutine(PlayLevel2Music());
    }

    IEnumerator PlayLevel2Music() {
        yield return new WaitForSeconds(0.2f);
        BackgroundMusic2.volume +=.1f;
        BackgroundMusic3.volume += .1f;

        if(BackgroundMusic2.volume == 1)
            StopCoroutine(PlayLevel2Music());
    }

    

    public void PlayBackgroundMusic3()
    {
        StartCoroutine(PlayLevel3Music());
    }

    IEnumerator PlayLevel3Music()
    {
        yield return new WaitForSeconds(0.2f);
        BackgroundMusic4.volume += .1f;

        if (BackgroundMusic4.volume == 1)
            StopCoroutine(PlayLevel3Music());
    }

}