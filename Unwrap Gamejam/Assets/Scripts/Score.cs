using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private float _scoreCount;
    public float MetersAway;
    public float ExplosionSize;
    [SerializeField] private Text MeterText;
    [SerializeField] private Text DestructionText;

    void Start()
    {
        _scoreCount = 0;
        MeterText = GameObject.Find("MeterText").GetComponent<Text>();
        DestructionText = GameObject.Find("DestructionText").GetComponent<Text>();

    }

    public void CalculateScore(Vector3 explosionPos)
    {
        var targetPos = GameObject.FindWithTag("Target").transform.position;
        var distance = (targetPos - explosionPos).sqrMagnitude;
        MetersAway = Mathf.Sqrt(distance);
        ExplosionSize = (gameObject.GetComponent<PlayerBehaviour>().SizeMultiplier + 0.01f) * 15;
        //var explosion =
        Debug.Log("The distance is:" + distance + "you were " + MetersAway + " away and the size was " + ExplosionSize);
        MeterText.text = $"You landed {MetersAway}m away!";
        DestructionText.text = $"You destructed {ExplosionSize*10}m of enemy territory!";

        // case 0m away, 0 size --) 
        // you destroyed the target!
        //if (ExplosionSize < MetersAway) Debug.Log(ExplosionSize + " " + MetersAway + "m");
    }
}
