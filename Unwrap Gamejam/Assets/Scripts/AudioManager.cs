using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    [SerializeField] Slider volumeSlider;

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
    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
        Save();
    }

    public void Save()
    {
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
    }

    public void SetSliderToCorrectAmount()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }
    void Start()
    {
        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
            SetSliderToCorrectAmount();
        }
        else
        {
            SetSliderToCorrectAmount();
        }
        _rocketAudio = GameObject.FindWithTag("Player").GetComponent<AudioSource>();
        _cameraAudio = Camera.main.GetComponent<AudioSource>();
        PlayLaunchSequence();
        StartRocketFlyingSound();
    }

    public void PlayLaunchSequence()
    {
        _cameraAudio.PlayOneShot(_rocketStart /* , _mainVolume * _sfxVolume */);
    }
    public void StartRocketFlyingSound()
    {
        _rocketAudio.PlayOneShot(_rocketFlying /* , _mainVolume * _ambientVolume */);
    }
}
