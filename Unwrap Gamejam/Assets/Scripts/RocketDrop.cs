using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketDrop : MonoBehaviour
{
    private void Awake()
    {
        gameObject.AddComponent<Rigidbody>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 3)
        {
            Debug.Log("kaboom");
        }
    }
}