using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class HomingMissle : MonoBehaviour
{
    private float _speed = 110;
    private float _rotateSpeed = 50;
    private Rigidbody _rb;
    private GameObject _player;
    private Vector3 _heading;
    private Quaternion _rotation;
    [SerializeField] private GameObject _explosionVFX;
    private GameObject _explosion;
    void Start()
    {
       _rb = GetComponent<Rigidbody>();
       _player = GameObject.Find("Player");
    }

    
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        Thurst();
        RotateMissle();
    }
    private void Thurst()
    {
        _rb.velocity = transform.forward * _speed;
    }
    private void RotateMissle()
    {
        _heading = _player.transform.position - transform.position;

        _rotation = Quaternion.LookRotation(_heading);
        _rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, _rotation, _rotateSpeed * Time.fixedDeltaTime));
    }
    private void OnCollisionEnter(Collision collision)
    {
        _explosion = GameObject.Instantiate(_explosionVFX, transform.position, Quaternion.identity);
        Destroy(_explosion, 1);
        Destroy(gameObject);
    }
}
