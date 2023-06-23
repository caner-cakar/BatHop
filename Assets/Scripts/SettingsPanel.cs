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
    public Sprite offButton;
    public Sprite onButton;



    private void Start() 
    {
        if(PlayerPrefs.GetInt("musicValue",1)==1)
        {
            musicButton.transform.GetChild(0).gameObject.SetActive(true);
            musicButton.transform.GetChild(1).gameObject.SetActive(false);
            musicBat.transform.GetChild(0).gameObject.SetActive(true);
            musicBat.transform.GetChild(1).gameObject.SetActive(false);
            musicButton.GetComponent<Image>().sprite = onButton;
        }
        else
        {
            musicButton.transform.GetChild(0).gameObject.SetActive(false);
            musicButton.transform.GetChild(1).gameObject.SetActive(true);
            musicBat.transform.GetChild(0).gameObject.SetActive(false);
            musicBat.transform.GetChild(1).gameObject.SetActive(true);
            musicButton.GetComponent<Image>().sprite = offButton;
        }
        if(PlayerPrefs.GetInt("soundValue",1)==1)
        {
            soundButton.transform.GetChild(0).gameObject.SetActive(true);
            soundButton.transform.GetChild(1).gameObject.SetActive(false);
            soundBat.transform.GetChild(0).gameObject.SetActive(true);
            soundBat.transform.GetChild(1).gameObject.SetActive(false);
            soundButton.GetComponent<Image>().sprite = onButton;
        }
        else
        {
            soundButton.transform.GetChild(0).gameObject.SetActive(false);
            soundButton.transform.GetChild(1).gameObject.SetActive(true);
            soundBat.transform.GetChild(0).gameObject.SetActive(false);
            soundBat.transform.GetChild(1).gameObject.SetActive(true);
            soundButton.GetComponent<Image>().sprite = offButton;
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
            musicButton.GetComponent<Image>().sprite = offButton;
            savedMusicValue = 0;
        }
        else
        {
            musicButton.transform.GetChild(0).gameObject.SetActive(true);
            musicButton.transform.GetChild(1).gameObject.SetActive(false);
            musicBat.transform.GetChild(0).gameObject.SetActive(true);
            musicBat.transform.GetChild(1).gameObject.SetActive(false);
            musicButton.GetComponent<Image>().sprite = onButton;
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
            soundButton.GetComponent<Image>().sprite = offButton;
            savedSoundValue = 0;
        }
        else
        {
            soundButton.transform.GetChild(0).gameObject.SetActive(true);
            soundButton.transform.GetChild(1).gameObject.SetActive(false);
            soundBat.transform.GetChild(0).gameObject.SetActive(true);
            soundBat.transform.GetChild(1).gameObject.SetActive(false);
            soundButton.GetComponent<Image>().sprite = onButton;
            savedSoundValue = 1;
        }
        PlayerPrefs.SetInt("soundValue",savedSoundValue);
    }
}
