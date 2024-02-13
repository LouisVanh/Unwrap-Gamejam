using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuel : MonoBehaviour
{
    private float FuelCount;
    public void AddFuel(float amount)
    {
        FuelCount += amount;
        if (FuelCount > 100) FuelCount = 100;
    }
    public bool HasFuel
    {
        get => FuelCount > 0;
        private set { }
    }
}
