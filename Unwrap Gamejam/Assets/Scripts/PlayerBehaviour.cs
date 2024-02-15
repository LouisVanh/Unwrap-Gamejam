using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEditor.Rendering;
using UnityEditor.Search;
using UnityEngine;

[RequireComponent(typeof(Fuel))]
public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] public float Fuel;
    [SerializeField] private float Speed = 100f;
    [SerializeField] private GameObject _trailVFX;
    [SerializeField] private GameObject _trailPos;
    [SerializeField] private GameObject _trail;
    public GameObject _mesh;
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
    private float _payload;
    private bool _isFlying = true; // turn to true on launch, just true now for testing
    private float _maxRotation = 5f;

    private float _gravity = -15f;
    public bool IsOn = true;
    private bool _engineOn = true;
    private float _cruiseSpeed = 110;
    private float _fuelBurnRate = -3.5f;
    private bool _reachedSpeed = false;
    private float _moveY;
    private float _moveX;
    [SerializeField] private float _mouseSensitivity = 30f;
    private Vector3 _aplhaRotation;
    public float SizeMultiplier;
    public Vector3 _originScale;

    private event EventHandler PlaySuperSpeedParticle;

    private void Awake()
    {
        //Application.targetFrameRate = 20;
        _superSpeedVFX1.GetComponent<ParticleSystem>().Play();
        _superSpeedVFX2.GetComponent<ParticleSystem>().Play();
        _rb = GetComponent<Rigidbody>();
        _fuel = GetComponent<Fuel>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _fuel.AddFuel(100); //max fuel
        PickupComponent.OnAddPayload += PickupComponent_OnAddPayload;
        _trail = GameObject.Instantiate(_trailVFX, _trailPos.transform.position, transform.rotation, transform);
        //_mouseSens = 5; //remove when we have final value, it's in the serializefield!
        //PlaySuperSpeedParticle += PlayerBehaviour_PlaySuperSpeedParticle;
        _originScale = transform.localScale;
        
    }

    private void PickupComponent_OnAddPayload(float value)
    {
        _payload += value;
        IncreasePayload();
    }

    //private void PlayerBehaviour_PlaySuperSpeedParticle(object sender, EventArgs e)
    //{
    //    _superSpeedVFX1.GetComponent<ParticleSystem>().Play();
    //    _superSpeedVFX2.GetComponent<ParticleSystem>().Play();
    //}

    void Update()
    {
        //Debug.Log(_payload);
        _mesh.transform.Rotate(0, 0, _meshRotSpeed * Time.deltaTime);
        //Debug.Log(_rb.velocity.magnitude);
        //_rb.velocity = transform.forward * Speed;
        //_rb.AddForce(new Vector3(0, _gravity, 0), ForceMode.Acceleration);
        
        GatherInput();
        FuelChecker();
        SuperSpeed();
        RocketEngineOnOf();
        //if (Input.GetKeyDown(KeyCode.Space) && _engineOn)
        //{
        //    IsOn = false;
        //    _engineOn = false;
        //}
        //else if (Input.GetKeyDown(KeyCode.Space) && !_engineOn)
        //{
        //    IsOn = true;
        //    _engineOn = true;
        //}
        if (_isFlying)
        {
            _fuel.AddFuel(_fuelBurnRate * Time.deltaTime);
            //Debug.Log($"do I have fuel?: {_fuel.HasFuel}");
        }
    }
    private void FixedUpdate()
    {
        _elapsedTime = Time.fixedDeltaTime;
        MoveRocket();
        
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
        if(_fuel.HasFuel)
        {
            IsOn= true;
            _rb.drag = 0.9f;
        }
    }
    private void CalculateGravity()
    {
        _rb.velocity -= Vector3.down * _gravity * Time.deltaTime;
    }
    private void GatherInput()
    {

        _moveX = Input.GetAxis("Mouse X") / 2;
        _moveY = Input.GetAxis("Mouse Y") / 2;



        _rotationX += _moveX;
        _rotationY += _moveY;

        //float rotX = _moveX * _mouseSensitivity;
        //float rotY = _moveY * _mouseSensitivity;

        //_rotationX= rotX;
        //_rotationY= rotY;

        //Debug.Log($"X :{rotX} Y :{rotY}");
    }
    private void MoveRocket()
    {
        float percentage = _elapsedTime * 1.8f ;
        percentage = Mathf.Clamp01(percentage);
        Quaternion toRot = Quaternion.Euler(_rotationY, _rotationX, _rb.rotation.z);
        //Quaternion newRot = _rb.rotation * toRot;

        Quaternion rotation = Quaternion.Lerp(_rb.rotation, toRot, percentage);
        _rb.rotation = rotation;

        _rb.MoveRotation(rotation);

        //_aplhaRotation = new Vector3(_moveY, _moveX, 0) *_mouseSensitivity;
        //Quaternion deltaRotation = Quaternion.Euler(_aplhaRotation);
        //Quaternion targetRotation = _rb.rotation * deltaRotation;

        ////Quaternion toRot = Quaternion.Lerp(_rb.rotation, targetRotation, 1);
        //Quaternion toRot = Quaternion.Slerp(_rb.rotation, targetRotation, 2 * Time.fixedDeltaTime);
        //_rb.MoveRotation(toRot);
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
    private void IncreasePayload()
    {
        SizeMultiplier = _payload / 200;
        Vector3 multiplier = new Vector3(_originScale.x / 10, _originScale.y / 10, _originScale.z / 10);
        transform.localScale += multiplier;
        _trailVFX.transform.localScale += new Vector3(SizeMultiplier, SizeMultiplier, SizeMultiplier);
    }
}
