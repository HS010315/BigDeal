using UnityEngine;
using UnityEngine.UI;

public class AudioVolumeControl : MonoBehaviour
{
    public Slider volumeSlider;
    public AudioSource audioSource;
    

    void Awake()
    {
        float savedVolume = PlayerPrefs.GetFloat("SavedVolume", 0.5f);
        audioSource.volume = savedVolume;
        volumeSlider.value = 1 - savedVolume; 
    }

    void Start()
    {
        volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
    }

    void OnVolumeChanged(float volume)
    {
        audioSource.volume = 1 - volume;

        PlayerPrefs.SetFloat("SavedVolume", audioSource.volume);
    }
}