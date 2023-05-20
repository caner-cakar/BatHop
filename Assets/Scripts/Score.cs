using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    Text highScoreText;
    Text currentScoreText;
    public Text DHighScoreText;
    public int currentScore = 0;

    private bool scoring = true;

    void Start()
    {
        highScoreText = GameObject.Find("HighScoreText").GetComponent<Text>();
        currentScoreText = GameObject.Find("CurrentScoreText").GetComponent<Text>();

        highScoreText.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
        currentScoreText.text = currentScore.ToString();
    }

    public void UpdateScore()
    {
        if(scoring)
        {
            currentScore++;
            currentScoreText.text = ""+currentScore;

            if(currentScore>PlayerPrefs.GetInt("HighScore", 0))
            {
                PlayerPrefs.SetInt("HighScore", currentScore);
                DHighScoreText.text = "New High Score: "+PlayerPrefs.GetInt("HighScore", 0).ToString();
                FindObjectOfType<GameSceneController>().isHighScore = true;
            }
            else
            {
                FindObjectOfType<GameSceneController>().isHighScore = false;
            }
        }
    }

    public void KeepScore()
    {
        scoring = true;
    }

    public void StopScore()
    {
        scoring = false;
    }
}
