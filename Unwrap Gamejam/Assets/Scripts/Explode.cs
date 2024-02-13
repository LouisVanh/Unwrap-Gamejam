using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    [SerializeField] private GameObject _prefabBig;
    [SerializeField] private GameObject _prefabSmall;

    private void Start()
    {

    }
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
        var big = Instantiate(_prefabSmall, this.transform.localPosition, Quaternion.identity);
        big.transform.localScale = 100 * Vector3.one;
        //maybe spawn multiple of these around it
        var small = Instantiate(_prefabBig, this.transform.localPosition, Quaternion.identity);
        small.transform.localScale = 5 * Vector3.one;

        // cam shake (already here but more)
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
