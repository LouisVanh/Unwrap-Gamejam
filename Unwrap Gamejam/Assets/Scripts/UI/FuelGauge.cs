using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelGauge : MonoBehaviour
{
    [SerializeField] private RectTransform pointer;


    [SerializeField] private float _minFuel = 10f;
    [SerializeField] private float _flashDelay = 0.1f;
    [SerializeField] private Image _pointerImage;
    [SerializeField] private Image _backgroundImage;

    private bool _doOnce = true;

    private float _fuelAmmount;

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
        _fuelAmmount = fuelammount;
        // Clamp the percentage between 0 and 100
        fuelammount = Mathf.Clamp(fuelammount, 0f, 100f);

        float angle = Mathf.Lerp(80f, -80f, fuelammount / 100f);

        // Apply rotation to the image transform
        pointer.rotation = Quaternion.Euler(0f, 0f, angle);

        if(fuelammount <= _minFuel && _doOnce) 
        {
            StartCoroutine(FlashFuel());

            _doOnce = false;
        }
    }

    private IEnumerator FlashFuel()
    {
        Color startColor = _pointerImage.color;
        bool flipFlop = false;

        while (_fuelAmmount <= _minFuel) 
        {
            flipFlop = !flipFlop;

            if(flipFlop )
            {
                _pointerImage.color = startColor;
                _backgroundImage.color = startColor;
            }
            else
            {
                _pointerImage.color = Color.red;
                _backgroundImage.color = Color.red;
            }           

            yield return new WaitForSeconds(_flashDelay);
        }

        _doOnce = true;
    }
}
