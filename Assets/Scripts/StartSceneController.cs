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

    public Text moneyScoreText;
    public void Awake()
    {
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
        
        startButton.SetActive(false);
        characterButton.SetActive(false);
        settingsButton.interactable = false;
        settingsPanel.SetActive(true);
        characterPanel.SetActive(false);
    }
    public void ExitSettings()
    {
        characterPanel.SetActive(false);
        settingsPanel.SetActive(false);
        startButton.SetActive(true);
        characterButton.SetActive(true);
        settingsButton.interactable = true;
    }

    public void CharacterPanel()
    {
        characterButton.SetActive(false);
        characterPanel.SetActive(true);
        settingsPanel.SetActive(false);
        startButton.SetActive(false);
        settingsButton.interactable = false;
        FindObjectOfType<CharacterSelect>().skins[PlayerPrefs.GetInt("SelectedCharacter",0)].SetActive(true);
        moneyScoreText.text=""+PlayerPrefs.GetInt("MoneyScore",0).ToString();
        Time.timeScale = 0;
    }

}