using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class CompassBar: MonoBehaviour
{
    [SerializeField] private RectTransform _compassBarTransform;
    [SerializeField] private RectTransform _objectiveMarkerTransform;
    [SerializeField] private GameObject _northMarker;
    [SerializeField] private GameObject _southMarker;
    [SerializeField] private GameObject _westMarker;
    [SerializeField] private GameObject _eastMarker;

    [SerializeField] private Transform _cameraobjectTransform;
    [SerializeField] private Transform _objectiveObjectTransform;

    [SerializeField] private float _angleOfDisapearing = 90;

    void Update()
    {
        SetMarkerPosition(_objectiveMarkerTransform, _objectiveObjectTransform.position);
        SetMarkerPositionDirections(_northMarker.GetComponent<RectTransform>(), Vector3.forward * 1000, _northMarker);
        SetMarkerPositionDirections(_southMarker.GetComponent<RectTransform>(), Vector3.back * 1000, _southMarker);

        SetMarkerPositionDirections(_westMarker.GetComponent<RectTransform>(), Vector3.left * 1000, _westMarker);
        SetMarkerPositionDirections(_eastMarker.GetComponent<RectTransform>(), Vector3.right * 1000, _eastMarker);
    }

    private void SetMarkerPosition(RectTransform markerTransform, Vector3 worldPosition)
    {
        Vector3 directionToTarget = worldPosition - _cameraobjectTransform.position;
        float angle = Vector2.SignedAngle(new Vector2(directionToTarget.x, directionToTarget.z), new Vector2(_cameraobjectTransform.transform.forward.x, _cameraobjectTransform.transform.forward.z));
        float compassPositionX = Mathf.Clamp(2 * angle / Camera.main.fieldOfView, -1, 1);
        markerTransform.anchoredPosition = new Vector2(_compassBarTransform.rect.width / 2 * compassPositionX, 0);
    }

    private void SetMarkerPositionDirections(RectTransform markerTransform, Vector3 worldPosition , GameObject marker)
    {
        Vector3 directionToTarget = worldPosition - _cameraobjectTransform.position;
        float angle = Vector2.SignedAngle(new Vector2(directionToTarget.x, directionToTarget.z), new Vector2(_cameraobjectTransform.transform.forward.x, _cameraobjectTransform.transform.forward.z));
        float compassPositionX = Mathf.Clamp(2 * angle / Camera.main.fieldOfView, -1, 1);
        markerTransform.anchoredPosition = new Vector2(_compassBarTransform.rect.width / 2 * compassPositionX, 0);

        if (angle > _angleOfDisapearing || angle < -_angleOfDisapearing)
        {
            marker.GetComponent<Text>().enabled = false;
        }
        else
        {
            marker.GetComponent<Text>().enabled = true;
        }
    }
}