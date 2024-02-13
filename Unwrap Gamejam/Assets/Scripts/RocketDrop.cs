using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketDrop : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 3)
        {

        }
    }
}
