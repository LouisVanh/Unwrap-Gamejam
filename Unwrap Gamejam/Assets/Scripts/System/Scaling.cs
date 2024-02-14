using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scaling : MonoBehaviour
{
    [SerializeField] private float _amplitude = 0.1f; // Amplitude of the pulsation.
    [SerializeField] private float _frequency = 1.0f; // Frequency of the pulsation.

    private Vector3 _initialScale;
    // Start is called before the first frame update
    void Start()
    {
        _initialScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        float scaleFactor = 1 + _amplitude * Mathf.Sin(2 * Mathf.PI * _frequency * Time.time);
        transform.localScale = _initialScale * scaleFactor;
    }
}
