using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{
    // 정수 : 천 단위로 구분 
    public static void ToStringFormattedNumber(this UILabel label, int number)
    {
        label.text = string.Format("{0:N0}", number);
    }
}
