using System;
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
    [SerializeField] private GameObject _mesh;
    [SerializeField] private GameObject _superSpeedVFX1;
    [SerializeField] private GameObject _superSpeedVFX2;
    private float _meshRotSpeed = 70;
    private Rigidbody _rb;
    private Vector3 _moveDirection;
    private Quaternion _moveRotation;
    private float _rotationX;
    private float _rotationY;
    private float _elapsedTime;
    private Fuel _fuel;
    private bool _isFlying = true; // turn to true on launch, just true now for testing
    private float _maxRotation = 5f;
    [SerializeField] private float _mouseSens;
    private float _gravity = -15f;
    public bool IsOn = true;
    private bool _engineOn = true;
    private float _cruiseSpeed = 110;
    private float _fuelBurnRate = -5;
    private bool _reachedSpeed = false;

    private event EventHandler PlaySuperSpeedParticle;

    private void Awake()
    {
        _superSpeedVFX1.GetComponent<ParticleSystem>().Play();
        _superSpeedVFX2.GetComponent<ParticleSystem>().Play();
        _rb = GetComponent<Rigidbody>();
        _fuel = GetComponent<Fuel>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _fuel.AddFuel(100); //max fuel
        _trail = GameObject.Instantiate(_trailVFX, _trailPos.transform.position, transform.rotation, transform);
        _mouseSens = 5; //remove when we have final value, it's in the serializefield!
        //PlaySuperSpeedParticle += PlayerBehaviour_PlaySuperSpeedParticle;
        
    }

    //private void PlayerBehaviour_PlaySuperSpeedParticle(object sender, EventArgs e)
    //{
    //    _superSpeedVFX1.GetComponent<ParticleSystem>().Play();
    //    _superSpeedVFX2.GetComponent<ParticleSystem>().Play();
    //}

    void Update()
    {

        _mesh.transform.Rotate(0, 0, _meshRotSpeed * Time.deltaTime);
        //Debug.Log(_rb.velocity.magnitude);
        //_rb.velocity = transform.forward * Speed;
        //_rb.AddForce(new Vector3(0, _gravity, 0), ForceMode.Acceleration);
        GatherInput();
        FuelChecker();
        MoveRocket();
        SuperSpeed();
        RocketEngineOnOf();
        if (Input.GetKeyDown(KeyCode.Space) && _engineOn)
        {
            IsOn = false;
            _engineOn = false;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && !_engineOn)
        {
            IsOn = true;
            _engineOn = true;
        }
        if (_isFlying)
        {
            _fuel.AddFuel(_fuelBurnRate * Time.deltaTime);
            //Debug.Log($"do I have fuel?: {_fuel.HasFuel}");
        }
    }
    private void FixedUpdate()
    {
        _elapsedTime = Time.fixedDeltaTime;
        if (IsOn)
        {

            _rb.velocity += transform.forward * Speed;
            if (_rb.velocity.magnitude > _cruiseSpeed)
            {
                _rb.velocity -= transform.forward * Speed * Time.fixedDeltaTime * 0.02f;
            }
            else
            {

            }

        }
        CalculateGravity();
        //Vector3 direction = transform.forward;
        //_moveDirection = Vector3.Normalize(direction) * Speed;
        //_rb.AddForce(_moveDirection, ForceMode.Impulse);

    }
    private void SuperSpeed()
    {
        if (_rb.velocity.magnitude >= 85)
        {
            PlaySuperSpeedParticle?.Invoke(this, EventArgs.Empty);
        }
    }
    private void FuelChecker()
    {
        if(!_fuel.HasFuel)
        {
            IsOn= false;
            _rb.drag = 0f;
        }
    }
    private void CalculateGravity()
    {
        _rb.velocity -= Vector3.down * _gravity * Time.deltaTime;
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
        //Quaternion toRot = Quaternion.Euler(_rotationY, _rotationX, _rb.rotation.z);

        //Quaternion rotation = Quaternion.Lerp(_rb.rotation, toRot, percentage);
        //_rb.rotation = rotation;
        Quaternion toRot = Quaternion.Euler(_rotationY, _rotationX, transform.rotation.z);

        Quaternion rotation = Quaternion.Lerp(transform.rotation, toRot, percentage);
        transform.rotation = rotation;
    }
    private void RocketEngineOnOf()
    {
        if (IsOn)
        {
            _trail.SetActive(true);
            Speed = 2;

        }
        if (!IsOn)
        {
            _trail.SetActive(false);
            Speed = 0;
        }
    }
}
