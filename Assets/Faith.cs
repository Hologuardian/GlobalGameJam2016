using UnityEngine;
using System.Collections;

public static class Faith
{
    private static float currentFaith = 75;
    public static float CurrentFaith 
    {
        get { Debug.Log(currentFaith); return currentFaith; }
        set { Debug.Log(currentFaith); currentFaith = value; } 
    }
    public static float MaxFaith = 1000;

    public const float cultistCost = 25;
    public const float sacrificeCost = 50;
    public const float obeliskCost = 100;
    public const float eclipseCost = 1000;

    public const float sacrificeGain = 5;
    public const float obeliskGain = 1;
    public const float obeliskTimer = 30;
}
