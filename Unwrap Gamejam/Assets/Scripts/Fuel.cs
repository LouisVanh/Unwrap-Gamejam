using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuel : MonoBehaviour
{
    private float FuelCount;

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
    }
    public bool HasFuel
    {
        get => FuelCount > 0;
        private set { }
    }
}
