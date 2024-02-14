using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningActivatorAndDeactivator : MonoBehaviour
{

    public delegate void WarningZoneEntered();
    public static event WarningZoneEntered OnWarningZoneEntered;

    public delegate void WarningZoneExit();
    public static event WarningZoneExit OnWarningZoneExit;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if(other.CompareTag("Player"))
        {
            OnWarningZoneEntered?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnWarningZoneExit?.Invoke();
        }
    }
}
