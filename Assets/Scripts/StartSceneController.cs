using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneController : MonoBehaviour
{
    [SerializeField] GameObject settingsPanel;
    public void Awake()
    {
        settingsPanel.SetActive(false);
    }
    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void Settings()
    {
        settingsPanel.SetActive(true);
        Time.timeScale = 0;
    }
    public void ExitSettings()
    {
        settingsPanel.SetActive(false);
        Time.timeScale = 1;
    }
}
