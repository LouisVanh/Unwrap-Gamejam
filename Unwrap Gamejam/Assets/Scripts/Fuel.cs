using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Fuel
{
    public static float FuelCount;
    public static void AddFuel(float amount)
    {
        FuelCount += amount;
    }
    public static bool HasFuel
    {
        get => FuelCount > 0;
        private set { }
    }
}
