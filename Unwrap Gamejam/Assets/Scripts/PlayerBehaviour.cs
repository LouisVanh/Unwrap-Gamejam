using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        //_rb.velocity = transform.forward * Speed;
        GatherInput();
        MoveRocket();
    }
    private void FixedUpdate()
    {
        _elapsedTime = Time.fixedDeltaTime;
    }
    private void GatherInput()
    {
        float moveX = Input.GetAxis("Mouse X") * 2;
        float moveY = Input.GetAxis("Mouse Y") * 2;
        _rotationX += moveX;
        _rotationY += moveY;
    }
    private void MoveRocket()
    {
        //float percentage = _elapsedTime;
        //_rb.rotation *= Quaternion.Euler(moveY, moveX, 0);
        //Quaternion toRot = Quaternion.Euler(_rotationY, _rotationX, -_rotationX);
        //transform.rotation *= Quaternion.Euler(moveY,moveX, 0);
        //_moveRotation = Quaternion.Lerp(transform.rotation, toRot, percentage);
        //transform.localRotation = _moveRotation;
        //Debug.Log(percentage);

        //float rotX = Input.GetAxisRaw("Horizontal") * 0.25f;
        //transform.localRotation *= Quaternion.Euler(0, 0, -rotX);
        var rotation = Quaternion.LookRotation(new Vector3(_rotationY, _rotationX, 0));
        _rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, rotation, 3 * Time.deltaTime));

    }
}
