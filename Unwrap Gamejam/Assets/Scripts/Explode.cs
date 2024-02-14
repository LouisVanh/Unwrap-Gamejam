using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    [SerializeField] private GameObject _prefabBig;
    [SerializeField] private GameObject _prefabSmall;
    private GameObject _player;

    [SerializeField] private Transform _camera;
    [SerializeField] private float _targetFOV = 70f;
    [SerializeField] private float _lerpSpeedFOV = 1f;
    [SerializeField] private float _targetCameraDistance = 50f;
    [SerializeField] private float _lerpSpeedCameraDistance = 1f;

    private void Start()
    {
        _player = GameObject.Find("Player");
        _camera = Camera.main.transform;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Missle")
        {
            //Debug.Log("Hit");
            StartCoroutine(PlayExplosion());
            _player.SetActive(false);
        }
        

    }

    private IEnumerator PlayExplosion()
    {
        //Debug.Log("BOOM");
        // vfx
        var big = Instantiate(_prefabBig, this.transform.position, Quaternion.identity);
        big.transform.localScale = 15 * Vector3.one;
        //maybe spawn multiple of these around it
        var small = Instantiate(_prefabSmall, this.transform.position, Quaternion.identity);
        small.transform.localScale = 5 * Vector3.one;

        // cam shake (already here but more)
        // sound
        // explosion force
        // terrain deformation
        // disable controls
        // camera look at explosion
        // camera lerp to view point (just add some value to the _targetCameraDistance)

        var parent = transform.parent;

        yield return new WaitForSeconds(0.05f);

        GetComponent<FOV>().enabled = false;
        GetComponent<PlayerBehaviour>().enabled = false;
        parent.GetComponentInChildren<CameraBehaviour>().enabled = false;

        var localposCamstart = _camera.position;
        //Vector3 localposCamtarget = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z + _targetCameraDistance);
        Vector3 localposCamtarget = localposCamstart - transform.forward * _targetCameraDistance;

        while (Mathf.Abs(Camera.main.fieldOfView - _targetFOV) > 0.01f /*|| Mathf.Abs(localposCamstart.z - localposCamtarget.z) > 0.01f*/)
        {
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, _targetFOV, _lerpSpeedFOV * Time.deltaTime);
            //localposCamstart.z = Mathf.Lerp(localposCamstart.z, localposCamtarget.z, _lerpSpeedCameraDistance);
            ////_camera.localPosition = localposCamstart;
            _camera.position = Vector3.Lerp(_camera.position, localposCamtarget, _lerpSpeedCameraDistance * Time.deltaTime);
            yield return null;
        }

        yield return new WaitForSeconds(10f);

        // show end screen (with stats of % destruction?)
    }
}