using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateWarningCanvas : MonoBehaviour
{
    void Start()
    {
        GetComponent<Canvas>().enabled = false;
    }


    private void OnEnable()
    {
        WarningActivatorAndDeactivator.OnWarningZoneEntered += EnableCanvas;
        WarningActivatorAndDeactivator.OnWarningZoneExit += DisableCanvas;
    }

    private void OnDisable()
    {
        WarningActivatorAndDeactivator.OnWarningZoneEntered -= EnableCanvas;
        WarningActivatorAndDeactivator.OnWarningZoneExit -= DisableCanvas;
    }

    private void EnableCanvas()
    {
        GetComponent<Canvas>().enabled = true;
    }

    private void DisableCanvas()
    {
        GetComponent<Canvas>().enabled = false;
    }
}
