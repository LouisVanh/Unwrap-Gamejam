using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    private float _scoreCount;
    public float MetersAway;
    public float ExplosionSize;
    void Start()
    {
        _scoreCount = 0;
    }

    public void CalculateScore(Vector3 explosionPos)
    {
        var targetPos = GameObject.FindWithTag("Target").transform.position;
        var distance = (targetPos - explosionPos).sqrMagnitude;
        MetersAway = Mathf.Sqrt(distance);
        ExplosionSize = (gameObject.GetComponent<PlayerBehaviour>().SizeMultiplier + 0.01f) * 15;

        Debug.Log("The distance is:" + distance + "you were " + MetersAway + " away and the size was " + ExplosionSize);
        // case 0m away, 0 size --) 
        // you destroyed the target!
        if (ExplosionSize < MetersAway) Debug.Log(ExplosionSize + " " + MetersAway + "m");
    }
}
