using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Calculator
{
    public static int CalculateLevel(float totalExp)
    {
        // 간단한 레벨 계산 공식: 레벨 = 루트(총 경험치 / 100)
        return Mathf.FloorToInt(Mathf.Sqrt(totalExp / 100f)) + 1;
    }

    public static float CalculateExpPercentage(float totalExp)
    {
        int currentLevel = CalculateLevel(totalExp);
        float expForCurrentLevel = (currentLevel - 1) * (currentLevel - 1) * 100f;
        float expForNextLevel = currentLevel * currentLevel * 100f;
        
        return (totalExp - expForCurrentLevel) / (expForNextLevel - expForCurrentLevel);
    }

    public static int CalculateAttackPower()
    {
        return 0;
    }
}
