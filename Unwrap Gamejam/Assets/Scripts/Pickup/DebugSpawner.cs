using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugSpawner : MonoBehaviour
{

    [SerializeField] private GameObject _prefab;
    [SerializeField] private float _spawnDelay = 3f;

    private void Awake()
    {
        StartCoroutine(SpawnItems());
    }

    private IEnumerator SpawnItems()
    {
        while (true) 
        {
            Instantiate(_prefab, transform.position , transform.rotation);
            yield return new WaitForSeconds(_spawnDelay);
        }        
    }
}
