using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartSceneController : MonoBehaviour
{
    public Button settingsButton;
    public GameObject settingsPanel;
    public GameObject characterPanel;

    public GameObject startButton;
    public GameObject characterButton;
    public GameObject characters;

    public void Awake()
    {
        characters.SetActive(false);
        characterPanel.SetActive(false);
        settingsPanel.SetActive(false);
        startButton.SetActive(true);
        characterButton.SetActive(true);
        settingsButton.enabled = true;
    }
    public void StartGame()
    {
        SceneManager.LoadSceneAsync("GameScene");
    }

    public void Settings()
    {
        characters.SetActive(false);
        startButton.SetActive(false);
        characterButton.SetActive(false);
        settingsButton.interactable = false;
        settingsPanel.SetActive(true);
        characterPanel.SetActive(false);
    }
    public void ExitSettings()
    {
        characters.SetActive(false);
        characterPanel.SetActive(false);
        settingsPanel.SetActive(false);
        startButton.SetActive(true);
        characterButton.SetActive(true);
        settingsButton.interactable = true;
    }

    public void CharacterPanel()
    {
        characters.SetActive(true);
        characterButton.SetActive(false);
        characterPanel.SetActive(true);
        settingsPanel.SetActive(false);
        startButton.SetActive(false);
        settingsButton.interactable = false;
        Time.timeScale = 0;
    }

}