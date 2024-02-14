using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HeightMeter : MonoBehaviour
{
    [SerializeField] float _MaxHeight = 10000f;
    [SerializeField] Text _MaxHeightText = null;
    [SerializeField] Text _MidHeightText = null;
    [SerializeField] Slider _HeightMeter = null;


    GameObject _Player;

    // Start is called before the first frame update
    void Awake()
    {
        _MaxHeightText.text = Mathf.FloorToInt(_MaxHeight).ToString();
        _MidHeightText.text = Mathf.FloorToInt(_MaxHeight/2f).ToString();

        StartCoroutine(DelayedAwake());
    }

    private IEnumerator DelayedAwake()
    {
        yield return null;
        _Player = FindAnyObjectByType<PlayerBehaviour>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (_Player != null)
        {
            _HeightMeter.value = Mathf.Clamp(_Player.transform.position.y / _MaxHeight, 0.01f, 1f);
        }       
    }
}