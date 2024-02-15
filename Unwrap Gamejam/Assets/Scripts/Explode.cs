using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Explode;

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
    [SerializeField] private AudioSource _explosionAudio;


    private bool _doOnce = true;


    public delegate void RocketExploded();
    public static event RocketExploded OnRocketExploded;

    private void Start()
    {
        _player = GameObject.Find("Player");
        _camera = Camera.main.transform;
        _explosionAudio = GameObject.Find("ExplosionSound").GetComponent<AudioSource>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if ((collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Missle") || collision.gameObject.CompareTag("Target"))&& _doOnce)
        {
            StartCoroutine(PlayExplosion(collision));
            _doOnce = false;
            //_player.SetActive(false);
        }
    }

    private IEnumerator PlayExplosion(Collision collision)
    {
        //Debug.Log("BOOM");
        //calculate score
        _player.GetComponent<Score>().CalculateScore(collision.transform.position);
        
        // vfx
        var big = Instantiate(_prefabBig, this.transform.position, Quaternion.identity);
        big.transform.localScale = (_player.GetComponent<PlayerBehaviour>().SizeMultiplier + 0.01f) * 15 * Vector3.one;
        
        //maybe spawn multiple of these around it
        var small = Instantiate(_prefabSmall, this.transform.position, Quaternion.identity);
        small.transform.localScale = 5 * Vector3.one;

        // cam shake (already here but more)
        // sound
        _explosionAudio.Play();
        // explosion force
        // terrain deformation
        // disable controls
        _player.GetComponent<PlayerBehaviour>()._mesh.SetActive(false);
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
        _player.SetActive(false);

        OnRocketExploded?.Invoke();
        // show end screen (with stats of % destruction?)
    }
}