using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Script : MonoBehaviour
{
    private Canvas _settings;
    private Canvas _menu = null;

    public Slider _slider = null;
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
        //AudioManager.Instance.SaveSliders();
        SceneManager.LoadScene(1);
        AudioManager.Instance.PleaseStartAttachingAudio();
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void RestartGame()
    {
        //AudioManager.Instance.SaveSliders();
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
        Time.timeScale = _settings.enabled ? 0 : 1;
        AudioManager.Instance.LoadSliders();

        if (SceneManager.GetActiveScene() != SceneManager.GetSceneByBuildIndex(0)) // MAIN MENU
        {
            Cursor.lockState = _settings.enabled ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = _settings.enabled ? true : false;
        }
        
        //Invoke(nameof(LinkSliders),1);

    }

    //private void LinkSliders()
    //{
    //    // when settings open, (re)link the sliders to the audiomanager
    //    //AudioManager.Instance.AmbientVolumeSlider = GameObject.Find("AmbientVolumeSlider").GetComponent<Slider>();
    //    //AudioManager.Instance.SFXVolumeSlider = GameObject.Find("SFXVolumeSlider").GetComponent<Slider>();
    //    //AudioManager.Instance.MainVolumeSlider = GameObject.Find("MainVolumeSlider").GetComponent<Slider>();
    //}

    //public void UpdateSlider()
    //{
    //    AudioManager.Instance.ChangeVolumes();
    //    Debug.Log("KILL YOURSELF");
    //}
}