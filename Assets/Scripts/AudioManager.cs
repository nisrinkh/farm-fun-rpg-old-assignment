using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Background Music")]
    public AudioClip backgroundMusic;
    [Range(0f, 1f)] public float musicVolume = 0.3f;

    private AudioSource musicSource;

    private void Awake()
    {
        // Singleton: pastikan hanya satu instance
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Buat AudioSource sekali saja
        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.clip = backgroundMusic;
        musicSource.volume = musicVolume;
        musicSource.loop = true;
        musicSource.playOnAwake = false; // JANGAN putar otomatis

        // Putar musik hanya jika belum pernah diputar
        if (!musicSource.isPlaying)
        {
            musicSource.Play();
        }
    }

    public void PlayMusic()
    {
        if (musicSource == null)
        {
            musicSource = gameObject.GetOrAddComponent<AudioSource>();
            musicSource.clip = backgroundMusic;
            musicSource.volume = musicVolume;
            musicSource.loop = true;
            musicSource.playOnAwake = false;
        }

        if (backgroundMusic != null && !musicSource.isPlaying)
        {
            musicSource.Play();
        }
    }

    // Pause BGM
    public void PauseMusic()
    {
        if (musicSource != null && musicSource.isPlaying)
        {
            musicSource.Pause();
        }
    }

    // Resume BGM
    public void ResumeMusic()
    {
        if (musicSource != null && !musicSource.isPlaying)
        {
            musicSource.UnPause(); // Lanjutkan dari posisi terakhir
        }
    }

    // Stop total (opsional)
    public void StopMusic()
    {
        if (musicSource != null)
        {
            musicSource.Stop();
        }
    }
}