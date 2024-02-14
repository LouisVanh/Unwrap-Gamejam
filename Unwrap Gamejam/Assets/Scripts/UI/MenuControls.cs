using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Script : MonoBehaviour
{
    private Canvas _settings;
    private Canvas _menu = null;
    private void Start()
    {
        // make sure the settings canvas has the settings tag!
        _settings = GameObject.FindWithTag("Settings").GetComponent<Canvas>();
        _settings.enabled = false;
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(0)) // MAIN MENU
        {
            _menu = GameObject.FindWithTag("Menu").GetComponent<Canvas>();
        }

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Settings();
        }
    }
    public void PlayGame()
    {
        AudioManager.Instance.Save();
        SceneManager.LoadScene(1);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void RestartGame()
    {
        AudioManager.Instance.Save();
        SceneManager.LoadScene(0);
        _menu.enabled = true;
        _settings.enabled = false;
    }

    public void Settings()
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(0)) // MAIN MENU
        {
            _menu.enabled = _settings.enabled ? true : false;
        }
        _settings.enabled = _settings.enabled ? false : true;
    }
}