using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneController : MonoBehaviour
{
    public PlayerMovement playerMovement;

    [SerializeField] GameObject settingsCanvas;
    [SerializeField] GameObject deathCanvas;

    private GameObject losePanel;
    private GameObject highScorePanel;
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
        deathCanvas.SetActive(true);
        losePanel.SetActive(true);
        highScorePanel.SetActive(false);
    }

    public void HighScorePanel()
    {
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


    public void DeathLogic()
    {
        Camera.main.transform.position = new Vector3(Camera.main.transform.position.x,Camera.main.transform.position.y,Camera.main.transform.position.z);
        Score score1 = gameObject.AddComponent<Score>();
        score1.StopScore();
        LosePanel();
    }
}
