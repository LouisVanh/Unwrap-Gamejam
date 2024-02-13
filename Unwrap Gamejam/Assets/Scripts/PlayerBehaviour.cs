using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Fuel))]
public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] public float Fuel;
    [SerializeField] private float Speed = 100f;
    [SerializeField] private GameObject _trailVFX;
    [SerializeField] private GameObject _trailPos;
    [SerializeField] private GameObject _trail;
    private Rigidbody _rb;
    private Vector3 _moveDirection;
    private Quaternion _moveRotation;
    private float _rotationX;
    private float _rotationY;
    private float _elapsedTime;
    private Fuel _fuel;
    private bool _isFlying = true; // turn to true on launch, just true now for testing
    private float _maxRotation = 5f;
    private float _gravity = -6f;
    public bool IsOn = true;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _fuel = GetComponent<Fuel>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _fuel.AddFuel(100); //max fuel
        _trail = GameObject.Instantiate(_trailVFX, _trailPos.transform.position, transform.rotation, transform);

    }


    void Update()
    {
        //_rb.velocity = transform.forward * Speed;
        _rb.AddForce(new Vector3(0, _gravity, 0), ForceMode.Acceleration);
        GatherInput();
        MoveRocket();
        RocketEngineOnOf();
        if (Input.GetKeyDown(KeyCode.Space) && IsOn)
        {
            IsOn = false;
        }
        if (_isFlying)
        {
            _fuel.AddFuel(-1 * Time.deltaTime);
            Debug.Log($"do I have fuel?: {_fuel.HasFuel}");
        }
    }
    private void FixedUpdate()
    {
        _elapsedTime = Time.fixedDeltaTime;
        if (IsOn)
        {
            _rb.velocity = transform.forward * Speed;

        }
        //Vector3 direction = transform.forward;
        //_moveDirection = Vector3.Normalize(direction) * Speed;
        //_rb.AddForce(_moveDirection, ForceMode.Impulse);

    }
    private void GatherInput()
    {
        float moveX = Input.GetAxis("Mouse X") / 2;
        float moveY = Input.GetAxis("Mouse Y") / 2;
        _rotationX += moveX;
        _rotationY += moveY;
    }
    private void MoveRocket()
    {
        float percentage = _elapsedTime / 1.5f;
        percentage = Mathf.Clamp01(percentage);
        Quaternion toRot = Quaternion.Euler(_rotationY, _rotationX, _rb.rotation.z);

        Quaternion rotation = Quaternion.Lerp(_rb.rotation, toRot, percentage);
        _rb.rotation = rotation;
    }
    private void RocketEngineOnOf()
    {
        if (IsOn)
        {
            _trail.SetActive(true);
            Speed = 100;
        }
        if (!IsOn)
        {
            _trail.SetActive(false);
            Speed = 0;
        }
    }
}
