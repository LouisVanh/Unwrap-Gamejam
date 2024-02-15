using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    private float _scoreCount;
    void Start()
    {
        _scoreCount = 0;
    }

    public void CalculateScore(Vector3 explosionPos)
    {
        var targetPos = GameObject.FindWithTag("Target").transform.position;
        var distance = targetPos - explosionPos;
        Debug.Log("The distance is:" + distance);
    }
}
