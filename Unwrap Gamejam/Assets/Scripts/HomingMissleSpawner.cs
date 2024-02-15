using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingMissleSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float _radius = 10;
    [SerializeField] private float _amountOfMissles = 5;
    [SerializeField] private GameObject _missle;
    private float _minHeightOffset = 100;
    private bool _spawned = false;
    private float _timer;
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;
        Debug.Log(_timer);
        if(_timer > 5 && !_spawned)
        {
            SpawnMissles();
            _spawned = true;
        }
    }
    private void SpawnMissles()
    {
        for (int i = 0; i< _amountOfMissles; i++)
        {
            Vector3 randomPos = Random.insideUnitSphere * _radius;
            _minHeightOffset = Random.Range(_minHeightOffset, 300);
            randomPos = new Vector3(randomPos.x, _minHeightOffset, randomPos.z);

            GameObject missle = GameObject.Instantiate(_missle, randomPos, Quaternion.identity);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(this.transform.position, _radius);
    }
}
