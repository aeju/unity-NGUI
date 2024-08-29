using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryDisplay : MonoBehaviour
{
    public UILabel batteryLabel;
    public UISprite chargingSprite;
    
    private BatteryStatus previousStatus;
    
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
        
        // 초기 상태 설정
        UpdateChargingSprite(SystemInfo.batteryStatus == BatteryStatus.Charging);

        StartCoroutine(UpdateBatteryStatus());
    }

    IEnumerator UpdateBatteryStatus()
    {
        while (true)
        {
            float batteryLevel = SystemInfo.batteryLevel;
            int batteryPercentage = Mathf.RoundToInt(batteryLevel * 100f);

            batteryLabel.text = string.Format("{0}%", batteryPercentage);

            // 충전 상태 확인
            BatteryStatus currentStatus = SystemInfo.batteryStatus;
            
            // 상태 변화 감지 및 처리
            if (currentStatus != previousStatus)
            {
                if (currentStatus == BatteryStatus.Charging && previousStatus == BatteryStatus.Discharging)
                {
                    UpdateChargingSprite(true);
                }
                else if (currentStatus == BatteryStatus.Discharging && previousStatus == BatteryStatus.Charging)
                {
                    UpdateChargingSprite(false);
                }
                
                previousStatus = currentStatus;
            }

            // 충전 상태 로그 출력
            Debug.Log($"Battery Status: {currentStatus}, Battery Level: {batteryPercentage}%");
            
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
}
