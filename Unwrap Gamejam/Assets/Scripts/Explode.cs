using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    [SerializeField] private GameObject _prefabBig;
    [SerializeField] private GameObject _prefabSmall;
    [SerializeField] private float _targetFOV = 70f;
    [SerializeField] private float _lerpSpeedFOV = 1f;
    [SerializeField] private float _targetCameraDistance = 50f;
    [SerializeField] private float _lerpSpeedCameraDistance = 1f;

    private void Start()
    {

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            //Debug.Log("Hit");
            StartCoroutine(PlayExplosion());
        }
    }

    private IEnumerator PlayExplosion()
    {
        //Debug.Log("BOOM");
        // vfx
        var big = Instantiate(_prefabSmall, this.transform.position, Quaternion.identity);
        big.transform.localScale = 30 * Vector3.one;
        //maybe spawn multiple of these around it
        var small = Instantiate(_prefabBig, this.transform.position, Quaternion.identity);
        small.transform.localScale = 5 * Vector3.one;

        // cam shake (already here but more)
        // sound
        // explosion force
        // terrain deformation
        // disable controls
        // camera look at explosion
        // camera lerp to view point (just add some value to the _targetCameraDistance)

        var parent = transform.parent;

        GetComponent<FOV>().enabled = false;
        GetComponent<PlayerBehaviour>().enabled = false;
        parent.GetComponentInChildren<CameraBehaviour>().enabled = false;

        var localposCamstart = parent.GetComponentsInChildren<Transform>()[1].localPosition;
        Vector3 localposCamtarget = new Vector3(localposCamstart.x, localposCamstart.y, localposCamstart.z + _targetCameraDistance);

        while (Mathf.Abs(Camera.main.fieldOfView - _targetFOV) > 0.01f && Mathf.Abs(localposCamstart.z - localposCamtarget.z) > 0.01f)
        {
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, _targetFOV, _lerpSpeedFOV * Time.deltaTime);
            localposCamstart.z = Mathf.Lerp(localposCamstart.z, localposCamtarget.z, _lerpSpeedCameraDistance);
            transform.parent.GetComponentsInChildren<Transform>()[1].localPosition = localposCamstart;
            yield return null;
        }
        yield return new WaitForSeconds(10f);
        
        // show end screen (with stats of % destruction?)
    }



}
