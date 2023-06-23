using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneController : MonoBehaviour
{
    [SerializeField] GameObject panels;
    [SerializeField] GameObject settingsPanel;
    [SerializeField] GameObject losePanel;
    [SerializeField] GameObject highScorePanel;
    [SerializeField] GameObject yourScorePanel;

    public bool isHighScore;
    public bool isYourScore;

    public void Awake()
    {
        panels.SetActive(false);
        settingsPanel.SetActive(false);
        losePanel.SetActive(false);
        highScorePanel.SetActive(false);
        yourScorePanel.SetActive(false);
    }

    public void Settings()
    {
        panels.SetActive(true);
        settingsPanel.SetActive(true);
        losePanel.SetActive(false);
        highScorePanel.SetActive(false);
        yourScorePanel.SetActive(false);
        Time.timeScale = 0;
    }
    public void ExitSettings()
    {
        panels.SetActive(false);
        settingsPanel.SetActive(false);
        losePanel.SetActive(false);
        highScorePanel.SetActive(false);
        yourScorePanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void LosePanel()
    {
        Time.timeScale = 1;
        panels.SetActive(true);
        settingsPanel.SetActive(false);
        losePanel.SetActive(true);
        highScorePanel.SetActive(false);
        yourScorePanel.SetActive(false);
    }

    public void HighScorePanel()
    {
        Time.timeScale = 1;
        panels.SetActive(true);
        settingsPanel.SetActive(false);
        losePanel.SetActive(false);
        highScorePanel.SetActive(true);
        yourScorePanel.SetActive(false);
    }
    public void YourScorePanel()
    {
        Time.timeScale = 1;
        panels.SetActive(true);
        settingsPanel.SetActive(false);
        losePanel.SetActive(false);
        highScorePanel.SetActive(false);
        yourScorePanel.SetActive(true);
    }

    public void PlayAgain()
    {
        Score score1 = gameObject.AddComponent<Score>();
        score1.KeepScore();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Menu()
    {
        SceneManager.LoadScene("StartScene");
    }


    public IEnumerator DeathLogic()
    {
        if(FindObjectOfType<BatsMovement>() != null)
            FindObjectOfType<BatsMovement>().isPlayerDead = true;
        FindObjectOfType<PlayerMovement>().isDead = true;
        FindObjectOfType<CameraController>().StopCamera();
        FindObjectOfType<Score>().StopScore();
        yield return new WaitForSeconds(0.5f);
        if(!isHighScore && !isYourScore)
            LosePanel();
        if(isYourScore)
            YourScorePanel();
        if(isHighScore)
            HighScorePanel();
    }
}
