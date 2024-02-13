using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            PlayExplosion();
        }
    }

    private void PlayExplosion()
    {
        Debug.Log("BOOM");
        // vfx
        // cam shake
        // sound
        // explosion force
        // terrain deformation
        // disable controls
        // camera look at explosion
        // camera lerp to view point
        // wait 10 seconds
        // show end screen (with stats of % destruction?)
    }

}
