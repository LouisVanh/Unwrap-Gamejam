using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [Header("Sounds")]
    [SerializeField] private AudioClip _explosion;
    [SerializeField] private AudioClip _wind;
    [SerializeField] private AudioClip _rocketFlying;
    [SerializeField] private AudioClip _homingFlying;
    [SerializeField] private AudioClip _homingMiss;
    [SerializeField] private AudioClip _UIClick;
    [SerializeField] private AudioClip _UIMiss;
    [SerializeField] private AudioClip _rocketStart;
    [SerializeField] private AudioClip _homingStart;
    // could put homing beep beep too?

    [Header("AudioSources (Debug, don't assign)")]
    [SerializeField] private AudioSource _rocketAudio;
    [SerializeField] private AudioSource _cameraAudio;

    [Header("Volume")] // implement todo (when sliders are here)
    [SerializeField] private float _mainVolume;
    [SerializeField] private float _sfxVolume;
    [SerializeField] private float _ambientVolume;


    public static AudioManager Instance;

    [SerializeField] AudioSource _menuAudio;
    [SerializeField] private AudioClip _clipGame;
    [SerializeField] private AudioClip _clipMenu;
    [SerializeField] private Slider _sfxVolumeSlider;
    [SerializeField] private Slider _mainVolumeSlider;
    [SerializeField] private Slider _ambientVolumeSlider;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(this);
    }
    void Start()
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(1)) // only ran if starting from in playscene - pretty much debug code
        {
            PleaseStartAttachingAudio();
        }

        if (!PlayerPrefs.HasKey("sfxVolume") || !PlayerPrefs.HasKey("mainVolume") || !PlayerPrefs.HasKey("ambientVolume")) //if the sliders aren't set yet
        {
            PlayerPrefs.SetFloat("sfxVolume", 1);
            PlayerPrefs.SetFloat("mainVolume", 1);
            PlayerPrefs.SetFloat("ambientVolume", 1);
        }
        LoadSliders();

        PlayCorrectMusicDependentOnScene();
    }
    public void PleaseStartAttachingAudio()
    {
        StartCoroutine(AttachAndPlayRocketSounds());
    }
    public IEnumerator AttachAndPlayRocketSounds()
    {
        yield return new WaitForSeconds(1f);
        _rocketAudio = GameObject.FindWithTag("Player").GetComponent<AudioSource>();
        _cameraAudio = Camera.main.GetComponent<AudioSource>();
        PlayLaunchSequence();
        StartRocketFlyingSound();
        yield return null;
    }

    private void PlayCorrectMusicDependentOnScene()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            //_menuAudio.clip = _clipMenu; //todo enable when we have music
        }
        else
        {
            //_menuAudio.clip = _clipGame; // todo enable when we have music
        }
        //_menuAudio.Play();
    }

    #region Volume shenanigans
    public void ChangeVolumes()
    {
        AudioListener.volume = _mainVolumeSlider.value;
        SaveSliders();
    }
    public void LoadSliders()
    {
        if (_mainVolumeSlider)
        _mainVolumeSlider.value = PlayerPrefs.GetFloat("mainVolume");
        if (_sfxVolumeSlider)
        _sfxVolumeSlider.value = PlayerPrefs.GetFloat("sfxVolume");
        if (_ambientVolumeSlider)
        _ambientVolumeSlider.value = PlayerPrefs.GetFloat("ambientVolume");
    }
    public void SaveSliders()
    {
        PlayerPrefs.SetFloat("mainVolume", _mainVolumeSlider.value);
        PlayerPrefs.SetFloat("sfxVolume", _sfxVolumeSlider.value);
        PlayerPrefs.SetFloat("ambientVolume", _ambientVolumeSlider.value);
    }
    #endregion


    #region Play sounds to audiosources
    public void PlayLaunchSequence()
    {
        _cameraAudio.PlayOneShot(_rocketStart /* , _mainVolume * _sfxVolume */);
    }
    public void StartRocketFlyingSound()
    {
        _rocketAudio.PlayOneShot(_rocketFlying /* , _mainVolume * _ambientVolume */);
    }
    #endregion
}
