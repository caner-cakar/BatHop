using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneController : MonoBehaviour
{
    public GameObject settingsPanel;
    public GameObject characterPanel;

    public GameObject startButton;
    public GameObject characterButton;
    public void Awake()
    {
        characterPanel.SetActive(false);
        settingsPanel.SetActive(false);
        startButton.SetActive(true);
        characterButton.SetActive(true);
    }
    public void StartGame()
    {
        SceneManager.LoadSceneAsync("GameScene");
    }

    public void Settings()
    {
        settingsPanel.SetActive(true);
        characterPanel.SetActive(false);
        startButton.SetActive(false);
        characterButton.SetActive(false);
        Time.timeScale = 0;
    }
    public void ExitSettings()
    {
        settingsPanel.SetActive(false);
        characterPanel.SetActive(false);
        startButton.SetActive(true);
        characterButton.SetActive(true);
        Time.timeScale = 1;
    }

    public void CharacterPanel()
    {
        settingsPanel.SetActive(false);
        characterPanel.SetActive(true);
        startButton.SetActive(false);
        characterButton.SetActive(false);
        Time.timeScale = 0;
    }

}