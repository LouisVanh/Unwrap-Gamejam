using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Fuel))]
public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] public float Fuel;
    [SerializeField] private float Speed = 100f;
    private Rigidbody _rb;
    private Vector3 _moveDirection;
    private Quaternion _moveRotation;
    private float _rotationX;
    private float _rotationY;
    private float _elapsedTime;
    private Fuel _fuel;
    private bool _isFlying = true; // turn to true on launch, just true now for testing
    private float _maxRotation = 45f;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _fuel = GetComponent<Fuel>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible= false;
        _fuel.AddFuel(100); //max fuel
    }


    void Update()
    {
        //_rb.velocity = transform.forward * Speed;
        GatherInput();
        MoveRocket();
        if (_isFlying)
        {
            _fuel.AddFuel(-1 * Time.deltaTime);
            Debug.Log($"do I have fuel?: {_fuel.HasFuel}");
        }
    }
    private void FixedUpdate()
    {
        _elapsedTime = Time.fixedDeltaTime;

        Vector3 direction = transform.forward;
        _moveDirection = Vector3.Normalize(direction) * Speed;
        _rb.AddForce(_moveDirection, ForceMode.Impulse);

    }
    private void GatherInput()
    {
        float moveX = Input.GetAxis("Mouse X") /2;
        float moveY = Input.GetAxis("Mouse Y") /2;
        _rotationX += moveX;
        _rotationY += moveY;
    }
    private void MoveRocket()
    {
        float percentage = _elapsedTime / 1.5f;
        percentage = Mathf.Clamp01(percentage);
        Quaternion toRot = Quaternion.Euler(_rotationY, _rotationX, transform.rotation.z);
        float rotationDifference = transform.rotation.y - toRot.y;
        
        Quaternion rotation = Quaternion.Lerp(transform.rotation, toRot, percentage);
        transform.rotation = rotation;
    }
}
