using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOV : MonoBehaviour
{
    private float _startFOV;
    private Rigidbody _rb;
    void Start()
    {
        _startFOV = Camera.main.fieldOfView;
        _rb = this.gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        var speed = _rb.velocity.magnitude * _rb.velocity.magnitude;
        //Debug.Log(speed);
        Camera.main.fieldOfView = Mathf.Clamp(speed / 10000 * 50, 60, 145); // lerp fov between 30 (very start / crash) and 125ish (top speed going straight down)
    }
}
