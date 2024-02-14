using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingMissleSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float _radius = 10;
    [SerializeField] private float _amountOfMissles = 5;
    [SerializeField] private GameObject _missle;
    private float _minHeightOffset = 70;
    void Start()
    {
        SpawnMissles();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void SpawnMissles()
    {
        for (int i = 0; i< _amountOfMissles; i++)
        {
            Vector3 randomPos = Random.insideUnitSphere * _radius;
            _minHeightOffset = Random.Range(_minHeightOffset, 150);
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
