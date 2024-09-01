using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeDisplay : MonoBehaviour
{
    public UILabel timeLabel; 
    public float updateInterval = 1f; // 업데이트 간격 (초)
    
    private float timer;
    
    private void Start()
    {
        if (timeLabel == null)
        {
            Debug.LogError("UILabel component is not assigned!");
            enabled = false;
            return;
        }

        UpdateTime(); // 초기 시간 설정
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= updateInterval)
        {
            UpdateTime();
            timer = 0f;
        }
    }

    private void UpdateTime()
    {
        DateTime koreanTime = DateTime.UtcNow.AddHours(9); // UTC+9 (한국 시간)
        timeLabel.text = koreanTime.ToString("HH:mm");
    }
}
