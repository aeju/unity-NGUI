using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    public AudioClip bgmClip;
    private AudioSource audioSource;

    void Awake()
    {
        // AudioSource 컴포넌트 추가 및 설정
        audioSource = this.gameObject.AddComponent<AudioSource>();
        audioSource.clip = bgmClip;
        audioSource.loop = true;
    }
    
    void Start()
    {
        PlayBGM();
    }

    public void PlayBGM()
    {
        if (audioSource != null && bgmClip != null)
        {
            audioSource.Play();
        }
    }

    public void StopBGM()
    {
        if (audioSource != null)
        {
            audioSource.Stop();
        }
    }

    public void SetBGMVolume(float volume)
    {
        if (audioSource != null)
        {
            audioSource.volume = Mathf.Clamp01(volume);
        }
    }
}
