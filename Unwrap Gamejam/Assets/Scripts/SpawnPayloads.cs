using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPayloads : MonoBehaviour
{
    private float _mapSize = 1500;
    [SerializeField]private GameObject _payload;
    // Start is called before the first frame update
    void Start()
    {
        Spawn(1000);
    }

    private void Spawn(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            float x = Random.Range(-1000, 1000);
            float y = Random.Range(-1000, 1000);
            float z = Random.Range(100, 1000);

            Instantiate(_payload, new Vector3(x, y, z), Quaternion.identity);
        }
    }
}
