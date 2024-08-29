using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryDisplay : MonoBehaviour
{
    public UILabel batteryLabel;

    void Start()
    {
        if (batteryLabel == null)
        {
            Debug.LogError("Battery UILabel is not assigned!");
            return;
        }

        StartCoroutine(UpdateBatteryStatus());
    }

    IEnumerator UpdateBatteryStatus()
    {
        while (true)
        {
            float batteryLevel = SystemInfo.batteryLevel;
            int batteryPercentage = Mathf.RoundToInt(batteryLevel * 100f);

            batteryLabel.text = string.Format("{0}%", batteryPercentage);

            yield return new WaitForSeconds(60f); // 1분마다 업데이트
        }
    }
}
