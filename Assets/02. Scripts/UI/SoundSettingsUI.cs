using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSettingsUI : MonoBehaviour
{
    public UISlider masterVolumeSlider;
    public UISlider bgmVolumeSlider;
    public UISlider sfxVolumeSlider;

    public UILabel masterVolumeLabel;
    public UILabel bgmVolumeLabel;
    public UILabel sfxVolumeLabel;

     void Start()
    {
        // 각 슬라이더 초기화
        InitializeSlider(masterVolumeSlider, "MasterVolume", OnMasterVolumeChanged);
        InitializeSlider(bgmVolumeSlider, "BGMVolume", OnBGMVolumeChanged);
        InitializeSlider(sfxVolumeSlider, "SFXVolume", OnSFXVolumeChanged);

        // 초기 볼륨 설정
        ApplyVolumeSettings();
    }

    void InitializeSlider(UISlider slider, string prefKey, UIEventListener.VectorDelegate onDragCallback)
    {
        if (slider != null)
        {
            // PlayerPrefs에서 저장된 값을 불러오거나 기본값 1 사용
            slider.sliderValue = PlayerPrefs.GetFloat(prefKey, 1f);
            // 전체 볼륨 : 연속적 / 나머지 : 5단계 
            slider.numberOfSteps = (prefKey == "MasterVolume") ? 0 : 6;
            // 드래그 이벤트 리스너 추가 
            UIEventListener.Get(slider.gameObject).onDrag += onDragCallback;
            // 클릭 이벤트 리스너 추가 
            UIEventListener.Get(slider.gameObject).onPress += (go, state) => { if (!state) onDragCallback(go, Vector2.zero); };
        }
    }

    // 각 볼륨 변경 콜백
    void OnMasterVolumeChanged(GameObject go, Vector2 delta)
    {
        UpdateVolumeSettings(masterVolumeSlider, "MasterVolume");
    }

    void OnBGMVolumeChanged(GameObject go, Vector2 delta)
    {
        UpdateVolumeSettings(bgmVolumeSlider, "BGMVolume");
    }

    void OnSFXVolumeChanged(GameObject go, Vector2 delta)
    {
        UpdateVolumeSettings(sfxVolumeSlider, "SFXVolume");
    }

    // 볼륨 설정 업데이트 
    void UpdateVolumeSettings(UISlider slider, string prefKey)
    {
        float value = slider.sliderValue; // 슬라이더 값
        PlayerPrefs.SetFloat(prefKey, value); // 볼륨 설정 저장
        ApplyVolumeSettings(); // 볼륨 설정 적용
        Debug.Log($"Updated {prefKey}: {value}");
    }

    // 볼륨 설정 적용 
    void ApplyVolumeSettings()
    {
        float masterVolume = PlayerPrefs.GetFloat("MasterVolume", 1f);
        float bgmVolume = PlayerPrefs.GetFloat("BGMVolume", 1f);
        float sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 1f);

        // BGM 볼륨 설정
        SoundManager.Instance.SetBGMVolume(masterVolume * bgmVolume);

        // SFX 볼륨 설정
        foreach (var sfx in SoundManager.Instance.soundEffects)
        {
            SoundManager.Instance.SetSFXVolume(sfx.name, masterVolume * sfxVolume);
        }
    }
    
    void Update()
    {
        UpdateVolumeLabel(masterVolumeLabel, masterVolumeSlider.sliderValue);
        UpdateVolumeLabel(bgmVolumeLabel, bgmVolumeSlider.sliderValue);
        UpdateVolumeLabel(sfxVolumeLabel, sfxVolumeSlider.sliderValue);
    }

    // 볼륨 라벨 업데이트 
    void UpdateVolumeLabel(UILabel label, float value)
    {
        if (label != null)
        {
            string newText = Mathf.RoundToInt(value * 100).ToString();
            if (label.text != newText)
            {
                label.text = newText;
                Debug.Log($"Updated label {label.name}: {newText}");
            }
        }
        else
        {
            Debug.LogWarning("Label is null in UpdateVolumeLabel");
        }
    }
}
