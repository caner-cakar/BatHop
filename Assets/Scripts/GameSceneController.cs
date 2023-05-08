using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneController : MonoBehaviour
{
    [SerializeField] GameObject settingsPanel;
    public void Awake()
    {
        settingsPanel.SetActive(false);
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
