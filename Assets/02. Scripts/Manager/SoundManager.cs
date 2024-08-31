using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    [Header("BGM Settings")]
    public AudioSource bgmSource;
    public float bgmVolume = 0.5f;

    [System.Serializable]
    public class SoundEffect
    {
        public string name;
        public AudioClip clip;
    }
    
    [Header("SFX Settings")]
    public List<SoundEffect> soundEffects;
    private Dictionary<string, AudioSource> sfxSources = new Dictionary<string, AudioSource>();

    protected override void Awake()
    {
        base.Awake();
        Init();
    }

    void Init()
    {
        // BGM
        if (bgmSource == null)
        {
            bgmSource = gameObject.AddComponent<AudioSource>();
        }
        bgmSource.loop = true;
        bgmSource.volume = bgmVolume;

        // SFX
        GameObject sfxContainer = new GameObject("SFX_Container");
        sfxContainer.transform.SetParent(transform);

        foreach (var sfx in soundEffects)
        {
            AudioSource source = sfxContainer.AddComponent<AudioSource>();
            source.clip = sfx.clip;
            source.playOnAwake = false;
            sfxSources.Add(sfx.name, source);
        }
    }
    
    void Start()
    {
        PlayBGM();
    }

    public void PlayBGM()
    {
        if (bgmSource.clip != null)
        {
            bgmSource.Play();
        }
        else
        {
            Debug.LogWarning("No BGM clip assigned to play.");
        }
    }

    public void StopBGM()
    {
        bgmSource.Stop();
    }

    public void SetBGMVolume(float volume)
    {
        bgmVolume = Mathf.Clamp01(volume);
        bgmSource.volume = bgmVolume;
    }
    
    public void PlaySFX(string name)
    {
        if (sfxSources.TryGetValue(name, out AudioSource source))
        {
            source.PlayOneShot(source.clip);
        }
        else
        {
            Debug.LogWarning($"Sound effect '{name}' not found. Available sounds: {string.Join(", ", sfxSources.Keys)}");
        }
    }

    public void SetSFXVolume(string name, float volume)
    {
        if (sfxSources.TryGetValue(name, out AudioSource source))
        {
            source.volume = Mathf.Clamp01(volume);
        }
    }
}
