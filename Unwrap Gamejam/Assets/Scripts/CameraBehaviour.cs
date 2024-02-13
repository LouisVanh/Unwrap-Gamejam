using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject _camTarget;
    [SerializeField] private GameObject _player;
    private float _elapsedTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.rotation = _player.transform.rotation;
    }
    private void FixedUpdate()
    {
        _elapsedTime = Time.fixedDeltaTime;
        MoveCamera();
    }
    private void MoveCamera()
    {
        float percentage = _elapsedTime/0.2f;
        transform.position = Vector3.Lerp(transform.position,_camTarget.transform.position, percentage);
        transform.rotation = Quaternion.Lerp(transform.rotation,_camTarget.transform.rotation, percentage);
    }
}
