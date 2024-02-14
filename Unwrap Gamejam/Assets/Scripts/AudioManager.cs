using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource _rocketAudio;
    [SerializeField] private AudioSource _cameraAudio;

    public static AudioManager Instance;

    [SerializeField] private AudioClip _rocketStart;
    [SerializeField] private AudioClip _rocketFlying;

    [SerializeField] private Slider MainVolumeSlider;
    //[SerializeField] private Slider SFXVolumeSlider;
    //[SerializeField] private Slider AmbientVolumeSlider;

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

    private void Start()
    {
        LoadSliders();
    }

    public void ChangeVolumes()
    {
        AudioListener.volume = MainVolumeSlider.value;
        SaveSliders();
    }

    public void LoadSliders()
    {
        if (PlayerPrefs.HasKey("mainVolume"))
        {
            MainVolumeSlider.value = PlayerPrefs.GetFloat("mainVolume");
        }
        else
        {
            MainVolumeSlider.value = 0.8f; // Default value
        }

        //if (PlayerPrefs.HasKey("sfxVolume"))
        //{
        //    SFXVolumeSlider.value = PlayerPrefs.GetFloat("sfxVolume");
        //}
        //else
        //{
        //    SFXVolumeSlider.value = 0.9f; // Default value
        //}

        //if (PlayerPrefs.HasKey("ambientVolume"))
        //{
        //    AmbientVolumeSlider.value = PlayerPrefs.GetFloat("ambientVolume");
        //}
        //else
        //{
        //    AmbientVolumeSlider.value = 0.8f; // Default value
        //}
    }

    public void SaveSliders()
    {
        PlayerPrefs.SetFloat("mainVolume", MainVolumeSlider.value);
        //PlayerPrefs.SetFloat("sfxVolume", SFXVolumeSlider.value);
        //PlayerPrefs.SetFloat("ambientVolume", AmbientVolumeSlider.value);
        PlayerPrefs.Save(); // Remember to save changes
    }

    public void PlayLaunchSequence()
    {
        _cameraAudio.PlayOneShot(_rocketStart);
    }

    public void StartRocketFlyingSound()
    {
        _rocketAudio.PlayOneShot(_rocketFlying);
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
}
