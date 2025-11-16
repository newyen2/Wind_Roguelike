using UnityEngine;

[CreateAssetMenu(menuName = "Audio/AudioData")]
public class AudioData : ScriptableObject
{
    public string key;
    public AudioClip clip;
    [Range(0f, 1f)] public float volume = 1f;
    public bool loop = false;
}
