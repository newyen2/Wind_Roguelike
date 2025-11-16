using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Play SFX : AudioManager.Instance.Play("key");

    // AudioManager.Instance.Play(loop = true, AudioManager.Instance.Play("key")); AudioData set to loop
    // AudioManager.Instance.Stop("key");

    public static AudioManager Instance { get; private set; }

    public AudioTable table;

    [Header("Shared source for SFX")]
    [SerializeField] private int sfxPoolSize = 5;

    private Dictionary<string, AudioSource> activeSources = new();
    private Queue<AudioSource> sfxPool = new();

    public float BGM_volume = 1, SFX_volume = 1;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        InitSFXPool();
    }

    private void InitSFXPool()
    {
        for (int i = 0; i < sfxPoolSize; i++)
        {
            var source = gameObject.AddComponent<AudioSource>();
            source.playOnAwake = false;
            source.spatialBlend = 0f;
            source.volume = 1f;
            source.pitch = 1f;
            sfxPool.Enqueue(source);
        }
    }

    public void Play(string key)
    {
        var data = table.GetAudio(key);
        if (data == null || data.clip == null)
        {
            return;
        }

        if (data.loop)
        {
            if (activeSources.ContainsKey(key))
            {
                return;
            }

            var loopSource = gameObject.AddComponent<AudioSource>();
            loopSource.clip = data.clip;
            loopSource.volume = data.volume;
            loopSource.volume *= BGM_volume;

            loopSource.loop = true;
            loopSource.spatialBlend = 0f;
            loopSource.Play();

            activeSources[key] = loopSource;
        }
        else
        {
            var source = GetSFXSource();
            source.clip = data.clip;
            source.volume = data.volume;
            source.volume *= SFX_volume;
            source.loop = false;
            source.spatialBlend = 0f;
            source.Play();
        }
    }

    public void Stop(string key)
    {
        if (activeSources.TryGetValue(key, out var source))
        {
            source.Stop();
            Destroy(source);
            activeSources.Remove(key);
        }
    }

    private AudioSource GetSFXSource()
    {
        if (sfxPool.Count > 0)
        {
            var source = sfxPool.Dequeue();
            StartCoroutine(ReturnToPoolAfterPlay(source));
            return source;
        }
        else
        {
            var temp = gameObject.AddComponent<AudioSource>();
            temp.spatialBlend = 0f;
            StartCoroutine(DestroyAfterPlay(temp));
            return temp;
        }
    }

    private IEnumerator ReturnToPoolAfterPlay(AudioSource source)
    {
        yield return new WaitUntil(() => source.isPlaying); // ����u�����_��

        float duration = source.clip != null ? source.clip.length : 0.1f;
        yield return new WaitForSeconds(duration);

        source.clip = null;
        sfxPool.Enqueue(source);
    }

    private System.Collections.IEnumerator DestroyAfterPlay(AudioSource source)
    {
        yield return new WaitWhile(() => source.isPlaying);
        Destroy(source);
    }
}
