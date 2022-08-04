using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    private void Awake()
    {
        SetupInstance();
    }

    public void SetupInstance()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }
    }

    public AudioSource[] music;
    public AudioSource[] sfx;

    public void PlayMusic(int trackNumber)
    {
        StopMusic();

        if(trackNumber < music.Length)
        {
            music[trackNumber].Play();
        }
    }

    public void StopMusic()
    {
        foreach(AudioSource track in music)
        {
            track.Stop();
        }
    }

    public void PlaySFX(int soundToPlay)
    {
        if(soundToPlay < sfx.Length)
        {
            sfx[soundToPlay].Stop();
            sfx[soundToPlay].Play();
        }
    }

    public void PlaySFXPitched(int soundToPlay)
    {
        sfx[soundToPlay].pitch = Random.Range(.8f, 1.2f);
        PlaySFX(soundToPlay);
    }
}
