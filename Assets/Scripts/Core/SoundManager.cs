using UnityEngine;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [Header("Audio Sources")]
    public AudioSource bgmSource;
    public AudioSource sfxSourcePrefab;   // 用來產生 SFX Player 的 prefab

    [Header("Audio Clips")]
    public List<SoundData> soundList;

    Dictionary<string, AudioClip> soundMap;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        soundMap = new Dictionary<string, AudioClip>();
        foreach (var item in soundList)
        {
            soundMap[item.name] = item.clip;
        }
    }

    // ---------------------------
    // BGM
    // ---------------------------

    public void PlayBGM(string name, float volume = 1f)
    {
        if (!soundMap.ContainsKey(name)) return;
        bgmSource.clip = soundMap[name];
        bgmSource.volume = volume;
        bgmSource.loop = true;
        bgmSource.Play();
    }

    public void StopBGM()
    {
        bgmSource.Stop();
    }

    // ---------------------------
    // SFX
    // ---------------------------

    public void PlaySFX(string name, float volume = 1f)
    {
        if (!soundMap.ContainsKey(name)) return;

        // 產生一個新的 AudioSource 播放 SFX
        var sfxObj = Instantiate(sfxSourcePrefab, transform);
        var src = sfxObj.GetComponent<AudioSource>();

        src.clip = soundMap[name];
        src.volume = volume;
        src.Play();

        Destroy(sfxObj.gameObject, src.clip.length + 0.1f);
    }
}

[System.Serializable]
public class SoundData
{
    public string name;
    public AudioClip clip;
}
