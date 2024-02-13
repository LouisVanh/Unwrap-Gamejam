using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugCamController : MonoBehaviour
{
    [SerializeField] private float sensitivity = 4f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        transform.Rotate(Vector3.up, mouseX * sensitivity);
        transform.Rotate(Vector3.left, mouseY * sensitivity);

        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0f);
    }
}
