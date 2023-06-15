using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] private AudioSource _musicSource, _effectsSource;
    [SerializeField] private AudioClip[] buttonSounds;
    
    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (PlayerPrefs.GetInt("Sound") == 0)
        {
            _musicSource.mute = true;
            _effectsSource.mute = true;
        }
    }

    public void PlaySound(AudioClip clip)
    {
        _effectsSource.PlayOneShot(clip);
    }

    public void PlaySoundWithVolume(AudioClip clip, float volume)
    {
        _effectsSource.PlayOneShot(clip, volume);
    }

    public void SetMusic(AudioClip clip)
    {
        _musicSource.PlayOneShot(clip);
    }

    public void ToggleMusic() => _musicSource.mute = !_musicSource.mute;
    
    public void ToggleEffects() => _effectsSource.mute = !_effectsSource.mute;
    
    public void ToggleAllSounds()
    {
        _musicSource.mute = !_musicSource.mute;
        _effectsSource.mute = !_effectsSource.mute;
    }

    public void SetMusicVolume(float volume) => _musicSource.volume = volume;
    
    public void SetEffectsVolume(float volume) => _effectsSource.volume = volume;

    public void PlayButtonSound() => _effectsSource.PlayOneShot(buttonSounds[Random.Range(0, buttonSounds.Length)]);

}
