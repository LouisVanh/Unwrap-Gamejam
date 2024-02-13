using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LauncheSequence : MonoBehaviour
{
    [SerializeField] private CameraBehaviour _camerBehavior;
    [SerializeField] private PlayerBehaviour _playerBehaviour;
    [SerializeField] private FOV _FOVbehavior;
    [SerializeField] private float _speed = 100f;

    [SerializeField] private float _launcheTime = 3f;
    [SerializeField] private float _controllsActivateTime = 3f;

    [SerializeField] private Rigidbody _rb;
    private Vector3 _moveDirection;

    private float _timerLaunch;
    private bool _IsLaunching = false;

    public delegate void TimeChanged(float time);
    public static event TimeChanged OnTimeChanged;


    // Start is called before the first frame update
    void Awake()
    {       
        _camerBehavior.enabled = false; 
        _playerBehaviour.enabled = false;
        _FOVbehavior.enabled = false;

        _timerLaunch = _launcheTime;

        Invoke("ActivateRocket", _launcheTime + _controllsActivateTime);
        Invoke("Launche", _launcheTime);
    }

    // Update is called once per frame
    void Update()
    {        
        _timerLaunch -= Time.deltaTime;
        OnTimeChanged?.Invoke(_timerLaunch);
    }

    private void FixedUpdate()
    {
        if (_IsLaunching)
        {
            Vector3 direction = transform.forward;
            _moveDirection = Vector3.Normalize(direction) * _speed;
            _rb.AddForce(_moveDirection, ForceMode.Impulse);
        }

    }

    private void ActivateRocket()
    {
        _camerBehavior.enabled = true;
        _playerBehaviour.enabled = true;
        _FOVbehavior.enabled = true;

        this.enabled = false;
    }

    private void Launche()
    {
        _IsLaunching = true;
    }
}
