using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource bgAudioSource;
    public AudioSource sfxAudioSource;

    public AudioClip almas;

    public Slider bgMusicVolSlider;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        bgMusicVolSlider.value = bgAudioSource.volume;
    }

    public void PlayAlmas()
    {
        PlaySound(almas);
    }

    private void PlaySound(AudioClip clip)
    {
        sfxAudioSource.PlayOneShot(clip);
    }

    public void ChangeVolume(float volume)
    {
        bgAudioSource.volume = bgMusicVolSlider.value;
    }
}
