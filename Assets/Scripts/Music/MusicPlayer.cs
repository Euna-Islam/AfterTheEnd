using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField]
    private SoundClip[] MusicClips;
    private AudioSource[] AudioSources = default;

    [SerializeField]
    private AudioSourceSettings MusicSettings;

    private void Start()
    {
        SetAudioClips();
    }

    void SetAudioClips() {
        AudioSources = new AudioSource[MusicClips.Length];
        for (int i = 0; i < MusicClips.Length; i++) {
            AudioSource audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = MusicClips[i].Clip;
            audioSource.playOnAwake = MusicSettings.PlayOnAwake;
            audioSource.mute = MusicSettings.Mute;
            audioSource.loop = MusicSettings.Loop;
            AudioSources[i] = audioSource;
        }
    }

    public void PlayMusic() {
        foreach (AudioSource source in AudioSources)
            source.Play();
    }
}
