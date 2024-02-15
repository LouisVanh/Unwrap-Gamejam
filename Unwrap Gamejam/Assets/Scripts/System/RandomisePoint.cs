using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RandomisePoint : MonoBehaviour
{
    private enum PlayerType
    {
        Player,
        Target
    }

    [SerializeField] private PlayerType _type;
    [SerializeField] private GameObject _spawnObject;
    [SerializeField] private List<Transform> _spawnPositions = new List<Transform>();   

    public delegate void TargetSet(GameObject target);
    public static event TargetSet OnTargetSet;

    // Start is called before the first frame update
    void Start()
    {
        if(_spawnPositions.Count > 0)
        {
            // Get a random index
            int randomIndex = Random.Range(0, _spawnPositions.Count);

            // Get the random object from the list
            Transform randompoint = _spawnPositions[randomIndex];

            //Debug.Log(randompoint.name);

            var spawnedObject = Instantiate(_spawnObject, randompoint.position, randompoint.rotation);

            switch(_type) 
            {
                case PlayerType.Player:
                    
                    break;
                case PlayerType.Target:
                    OnTargetSet?.Invoke(spawnedObject);
                    break;
                default:
                    break;
            }
            
        }
        else
        {
            Debug.LogWarning("The list is empty!");
        }
    }

    
}
