using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;

    bool musicPlay;

    private void Awake() 
    {
        if (Instance == null) 
        {
            Instance = this;
        }   
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update() 
    {
        if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            themeSound("Main");
        }
        if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            themeSound("Game");
        }
    }


    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicSounds, x=> x.name == name);

        if(s==null)
        {
            Debug.Log("Sound Not Found");
        }

        else 
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }

    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sfxSounds, x => x.name == name);

        if(s==null)
        {
            Debug.Log("Sound Not Found");
        }

        else 
        {
            sfxSource.PlayOneShot(s.clip);
        }
    }

    public void themeSound(string name)
    {
        if(PlayerPrefs.GetInt("musicValue",1)==1)
        {
            if(name == "Main" && !musicPlay)
            {
                AudioManager.Instance.PlayMusic("Main");
                musicPlay = true;
            }
                
            if(name == "Game" && !musicPlay)
            {
                AudioManager.Instance.PlayMusic("Game");
                musicPlay = true;
            }
        }
        else if(PlayerPrefs.GetInt("musicValue",1)==0)
        {
            musicPlay = false;
            AudioManager.Instance.musicSource.Stop();
        }
    }
}

