using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject _camTarget;
    [SerializeField] private GameObject _player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = _camTarget.transform.position;
        transform.rotation = _player.transform.rotation;
    }
}
