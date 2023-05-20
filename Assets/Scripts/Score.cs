using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    Text highScoreText;
    int highScore = 0;

    Text currentScoreText;
    public int currentScore = 0;

    private bool scoring = true;

    void Start()
    {
        highScoreText = GameObject.Find("HighScoreText").GetComponent<Text>();
        currentScoreText = GameObject.Find("CurrentScoreText").GetComponent<Text>();

        highScoreText.text = highScore.ToString();
        currentScoreText.text = currentScore.ToString();
    }

    public void UpdateScore()
    {
        if(scoring)
        {
            currentScore++;
            currentScoreText.text = ""+currentScore;
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
