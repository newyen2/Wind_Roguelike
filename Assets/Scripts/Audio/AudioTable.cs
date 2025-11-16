using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioTable", menuName = "Audio/AudioTable")]
public class AudioTable : ScriptableObject
{
    public List<AudioData> audioList;

    private Dictionary<string, AudioData> _lookup;

    public void Init()
    {
        _lookup = audioList.ToDictionary(a => a.key, a => a);
    }

    public AudioData GetAudio(string key)
    {
        if (_lookup == null) Init();
        _lookup.TryGetValue(key, out var audio);
        return audio;
    }
}