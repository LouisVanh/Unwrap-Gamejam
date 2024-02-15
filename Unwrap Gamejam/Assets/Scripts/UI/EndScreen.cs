using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndScreen : MonoBehaviour
{
    [SerializeField] Text _score;
    [SerializeField,Range(0f,20f)] float _EndScreenDelay = 0f;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Canvas>().enabled = false;
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.P))
        //{
        //    EnableEndScreen();
        //}
    }

    private void OnEnable()
    {
        Explode.OnRocketExploded += EnableEndScreen;
    }

    private void OnDisable()
    {
        Explode.OnRocketExploded += EnableEndScreen;
    }

    private void EnableEndScreen()
    {
       StartCoroutine(EnableEndscreenDelayed());        
    }

    private IEnumerator EnableEndscreenDelayed()
    {
        yield return new WaitForSeconds(_EndScreenDelay);
        GetComponent<Canvas>().enabled = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void ReloadScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);        
    }

    private void UpdateScore(float score)
    {
        _score.text = Mathf.Round(score).ToString();
    }

}
