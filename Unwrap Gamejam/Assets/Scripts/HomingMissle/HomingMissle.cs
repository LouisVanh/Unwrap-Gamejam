using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingMissle : MonoBehaviour
{
    private float _speed = 100;
    private float _rotateSpeed = 35;
    private Rigidbody _rb;
    public GameObject _player;
    private Vector3 _heading;
    private Quaternion _rotation;
    [SerializeField] private GameObject _explosionVFX;
    [SerializeField] private GameObject _mesh;
    private GameObject _explosion;
    private bool _foundPlayer;
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _player = GameObject.Find("Player");
    }


    void Update()
    {
        _mesh.transform.Rotate(0, 0, 140 * Time.deltaTime);
        if (_player == null)
        {
            _player = GameObject.Find("Player");
        }
        else 
        {
            _foundPlayer = true;
        }
    }
    private void FixedUpdate()
    {
        if (_foundPlayer)
        {
            Thurst();
            RotateMissle();

        }
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
