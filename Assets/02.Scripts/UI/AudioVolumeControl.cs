using UnityEngine;
using UnityEngine.UI;

public class AudioVolumeControl : MonoBehaviour
{
    public Slider volumeSlider;
    public AudioSource audioSource;
    

    void Start()
    {
        volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
        volumeSlider.value = 0.5f;
    }

    void OnVolumeChanged(float volume)
    {
        audioSource.volume = 1 - volume;
    }
}