using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PickupComponent;

public enum PickupType
{
    Fuel,
    Payload
}

public class PickupComponent : MonoBehaviour
{
    [SerializeField] private float _value = 10f;
    [SerializeField] private PickupType _type;

    private const string FRIENDLY_TAG = "Player";


    public delegate void AddFuel(float value);
    public static event AddFuel OnAddFuel;

    public delegate void AddPayload(float value);
    public static event AddPayload OnAddPayload;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(FRIENDLY_TAG)) return;

        switch (_type)
        {
            case PickupType.Fuel:
                OnAddFuel?.Invoke(_value);
                Debug.Log("Fuel is added");
                break;
            case PickupType.Payload:
                OnAddPayload?.Invoke(_value);
                Debug.Log("payload is added");
                break;
            default:
                break;
        }

        Destroy(gameObject);
    }
}
