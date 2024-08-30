using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Definitions;

public class BatteryDisplay : MonoBehaviour
{
    public UILabel batteryLabel;
    public UISprite chargingSprite;
    private BatteryStatus previousChargingStatus;
    
    public UISlider batterySlider;
    public UISprite sliderFillSprite;
    private BatterySliderColorStatus batterySliderColorStatus;
    
    /*
    public enum BatterySliderColorStatus
    {
        low, // ~20 : red
        medium, // ~40 : yellow
        high // ~100 : green 
    }
    */
    
    void Start()
    {
        if (batteryLabel == null)
        {
            Debug.LogError("Battery UILabel is not assigned!");
            return;
        }
        if (chargingSprite == null)
        {
            Debug.LogError("Battery UISprite is not assigned!");
            return;
        }
        if (batterySlider == null)
        {
            Debug.LogError("Battery UISlider is not assigned!");
            return;
        }
        if (sliderFillSprite == null)
        {
            Debug.LogError("Battery UISprite is not assigned!");
            return;
        }
        
        // 초기 상태 설정
        UpdateChargingSprite(SystemInfo.batteryStatus == BatteryStatus.Charging);
        UpdateBatterySlider(SystemInfo.batteryLevel);

        StartCoroutine(UpdateBatteryStatus());
    }

    IEnumerator UpdateBatteryStatus()
    {
        while (true)
        {
            float batteryLevel = SystemInfo.batteryLevel;
            int batteryPercentage = Mathf.RoundToInt(batteryLevel * 100f);

            batteryLabel.text = string.Format("{0}%", batteryPercentage);

            // 배터리 슬라이더 업데이트
            UpdateBatterySlider(batteryLevel);
            
            // 충전 상태 확인
            BatteryStatus currentChargingStatus = SystemInfo.batteryStatus;
            
            // 상태 변화 감지 및 처리
            if (currentChargingStatus != previousChargingStatus)
            {
                if (currentChargingStatus == BatteryStatus.Charging && previousChargingStatus == BatteryStatus.Discharging)
                {
                    UpdateChargingSprite(true);
                }
                else if (currentChargingStatus == BatteryStatus.Discharging && previousChargingStatus == BatteryStatus.Charging)
                {
                    UpdateChargingSprite(false);
                }
                
                previousChargingStatus = currentChargingStatus;
            }

            // 충전 상태 로그 출력
            Debug.Log($"Battery Status: {currentChargingStatus}, Battery Level: {batteryPercentage}%");
            
            yield return new WaitForSeconds(1f); // 1초마다 업데이트
        }
    }
    
    void UpdateChargingSprite(bool isCharging)
    {
        chargingSprite.alpha = isCharging ? 1f : 0f;

        // NGUI의 DrawCall을 최적화하기 위해 패널 업데이트
        if (chargingSprite.panel != null)
        {
            chargingSprite.panel.Refresh();
        }

        Debug.Log($"Charging sprite {(isCharging ? "activated" : "deactivated")}");
    }
    
    // 배터리 슬라이더 업데이트 (슬라이더 값, 슬라이더 색상)
    void UpdateBatterySlider(float batteryLevel)
    {
        batterySlider.sliderValue = batteryLevel; // 슬라이더 값 변환

        // 배터리 레벨에 따라 색상 변경
        BatterySliderColorStatus colorStatus = GetBatteryColorStatus(batteryLevel);
        
        // 상태에 따른 색상 가져오기
        Color sliderColor = GetColorForStatus(colorStatus);

        // 슬라이더 색상 업데이트 
        sliderFillSprite.color = sliderColor;

        Debug.Log($"Battery slider updated: Level {batteryLevel}, Color Status {colorStatus}");
    }
    
    // 배터리 레벨 - 색상 상태  
    BatterySliderColorStatus GetBatteryColorStatus(float batteryLevel)
    {
        if (batteryLevel <= 0.2f)
            return BatterySliderColorStatus.Low;
        else if (batteryLevel <= 0.4f)
            return BatterySliderColorStatus.Medium;
        else
            return BatterySliderColorStatus.High;
    }

    // 배터리 상태에 해당하는 색상 반환 
    Color GetColorForStatus(BatterySliderColorStatus status)
    {
        switch (status)
        {
            case BatterySliderColorStatus.Low:
                return Color.red;
            case BatterySliderColorStatus.Medium:
                return Color.yellow;
            case BatterySliderColorStatus.High:
                return Color.green;
            default:
                return Color.white; // 예외 처리
        }
    }
}
