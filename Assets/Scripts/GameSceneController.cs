using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneController : MonoBehaviour
{
    [SerializeField] GameObject settingsCanvas;
    [SerializeField] GameObject deathCanvas;

    private GameObject losePanel;
    private GameObject highScorePanel;

    public bool isHighScore;

    public void Awake()
    {
        settingsCanvas.SetActive(false);
        deathCanvas.SetActive(false);
        losePanel = deathCanvas.transform.Find("LosePanel").gameObject;
        highScorePanel = deathCanvas.transform.Find("HighScorePanel").gameObject;
    }

    private void Start() 
    {
    }

    public void Settings()
    {
        settingsCanvas.SetActive(true);
        Time.timeScale = 0;
    }
    public void ExitSettings()
    {
        settingsCanvas.SetActive(false);
        Time.timeScale = 1;
    }

    public void LosePanel()
    {
        Time.timeScale = 1;
        deathCanvas.SetActive(true);
        losePanel.SetActive(true);
        highScorePanel.SetActive(false);
    }

    public void HighScorePanel()
    {
        Time.timeScale = 1;
        deathCanvas.SetActive(true);
        highScorePanel.SetActive(true);
        losePanel.SetActive(false);
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
        if(!isHighScore)
            LosePanel();
        if(isHighScore)
            HighScorePanel();
    }
}
