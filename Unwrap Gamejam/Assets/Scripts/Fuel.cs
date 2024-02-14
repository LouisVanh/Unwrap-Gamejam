using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuel : MonoBehaviour
{
    private float FuelCount;

    public delegate void FuelammountChanged(float fuelcount);
    public static event FuelammountChanged OnFuelammountChanged;

    private void OnEnable()
    {
        PickupComponent.OnAddFuel += AddFuel;
    }
    private void OnDisable()
    {
        PickupComponent.OnAddFuel -= AddFuel;
    }

    public void AddFuel(float amount)
    {
        FuelCount += amount;
        if (FuelCount > 100) FuelCount = 100;
        OnFuelammountChanged?.Invoke(FuelCount);
    }
    public bool HasFuel
    {
        get => FuelCount > 0;
        private set { }
    }
}
