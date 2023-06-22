using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPanel : MonoBehaviour
{
    public GameObject musicButton;
    public GameObject musicBat;
    public GameObject soundButton;
    public GameObject soundBat;



    private void Start() 
    {
        if(PlayerPrefs.GetInt("musicValue",1)==1)
        {
            musicButton.transform.GetChild(0).gameObject.SetActive(true);
            musicButton.transform.GetChild(1).gameObject.SetActive(false);
            musicBat.transform.GetChild(0).gameObject.SetActive(true);
            musicBat.transform.GetChild(1).gameObject.SetActive(false);
        }
        else
        {
            musicButton.transform.GetChild(0).gameObject.SetActive(false);
            musicButton.transform.GetChild(1).gameObject.SetActive(true);
            musicBat.transform.GetChild(0).gameObject.SetActive(false);
            musicBat.transform.GetChild(1).gameObject.SetActive(true);
        }
        if(PlayerPrefs.GetInt("soundValue",1)==1)
        {
            soundButton.transform.GetChild(0).gameObject.SetActive(true);
            soundButton.transform.GetChild(1).gameObject.SetActive(false);
            soundBat.transform.GetChild(0).gameObject.SetActive(true);
            soundBat.transform.GetChild(1).gameObject.SetActive(false);
        }
        else
        {
            soundButton.transform.GetChild(0).gameObject.SetActive(false);
            soundButton.transform.GetChild(1).gameObject.SetActive(true);
            soundBat.transform.GetChild(0).gameObject.SetActive(false);
            soundBat.transform.GetChild(1).gameObject.SetActive(true);
        }
    }

    public void ChangeMusic()
    {
        int savedMusicValue = PlayerPrefs.GetInt("musicValue",1);
        if(savedMusicValue == 1)
        {
            musicButton.transform.GetChild(0).gameObject.SetActive(false);
            musicButton.transform.GetChild(1).gameObject.SetActive(true);
            musicBat.transform.GetChild(0).gameObject.SetActive(false);
            musicBat.transform.GetChild(1).gameObject.SetActive(true);
            savedMusicValue = 0;
        }
        else
        {
            musicButton.transform.GetChild(0).gameObject.SetActive(true);
            musicButton.transform.GetChild(1).gameObject.SetActive(false);
            musicBat.transform.GetChild(0).gameObject.SetActive(true);
            musicBat.transform.GetChild(1).gameObject.SetActive(false);
            savedMusicValue = 1;
        }
        PlayerPrefs.SetInt("musicValue",savedMusicValue);
    }

    public void ChangeSound()
    {
        int savedSoundValue = PlayerPrefs.GetInt("soundValue",1);
        if(savedSoundValue == 1)
        {
            soundButton.transform.GetChild(0).gameObject.SetActive(false);
            soundButton.transform.GetChild(1).gameObject.SetActive(true);
            soundBat.transform.GetChild(0).gameObject.SetActive(false);
            soundBat.transform.GetChild(1).gameObject.SetActive(true);
            savedSoundValue = 0;
        }
        else
        {
            soundButton.transform.GetChild(0).gameObject.SetActive(true);
            soundButton.transform.GetChild(1).gameObject.SetActive(false);
            soundBat.transform.GetChild(0).gameObject.SetActive(true);
            soundBat.transform.GetChild(1).gameObject.SetActive(false);
            savedSoundValue = 1;
        }
        PlayerPrefs.SetInt("soundValue",savedSoundValue);
    }
}
