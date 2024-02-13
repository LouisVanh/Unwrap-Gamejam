using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] public float Fuel;
    [SerializeField] private float Speed = 50f;
    private Rigidbody _rb;
    private Vector3 _moveDirection;
    private float _rotationX;
    private float _rotationY;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible= false;
    }


    void Update()
    {
        Vector3 direction = transform.forward;
        _moveDirection = Vector3.Normalize(direction) * Speed;
        _rb.AddForce(_moveDirection, ForceMode.Impulse);
        GatherInput();
    }
    private void GatherInput()
    {
        float moveX = Input.GetAxis("Mouse X") * 2;
        float moveY = Input.GetAxis("Mouse Y") * 2;
        _rotationX += moveX;
        _rotationY += moveY;
        _rb.rotation *= Quaternion.Euler(moveY, moveX, 0);
        transform.rotation *= Quaternion.Euler(moveY,moveX, 0);

    }
}
