using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelGauge : MonoBehaviour
{
    [SerializeField] private RectTransform pointer;

    private void OnEnable()
    {
        Fuel.OnFuelammountChanged += UpdatePointer;
    }

    private void OnDisable()
    {
        Fuel.OnFuelammountChanged -= UpdatePointer;
    }

    void UpdatePointer(float fuelammount)
    {
        // Clamp the percentage between 0 and 100
        fuelammount = Mathf.Clamp(fuelammount, 0f, 100f);

        float angle = Mathf.Lerp(80f, -80f, fuelammount / 100f);

        // Apply rotation to the image transform
        pointer.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}
