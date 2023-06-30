using UnityEngine;

[CreateAssetMenu(menuName = "Audio/Settings")]
public class AudioSourceSettings : ScriptableObject
{
    public bool Mute;
    public bool Loop;
    public bool PlayOnAwake;
}
