using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LauncheTimer : MonoBehaviour
{
    [SerializeField] private Text _text;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        LauncheSequence.OnTimeChanged += UpdateTimer;
    }

    private void OnDisable() 
    {
        LauncheSequence.OnTimeChanged -= UpdateTimer;
    }

    void UpdateTimer(float time)
    {
        _text.text = Mathf.FloorToInt(time).ToString();

        if(time <= 0)
        {
            _text.enabled = false;
            this.enabled = false;
        }
    }
}
