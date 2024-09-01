using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public enum Side
    {
        Left,
        Right,
        // Center,
    }
    
    public Side selectedSide = Side.Left;
}
