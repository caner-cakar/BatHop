using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Score : MonoBehaviour
{
    TextMeshProUGUI currentScoreText;
    TextMeshProUGUI coinScoreText;
    TextMeshProUGUI scoreText;
    public int moneyScore = 0;
    public int currentScore = 0;

    [SerializeField] TextMeshProUGUI yourScoreText;
    [SerializeField] TextMeshProUGUI highScoreText;
    GameObject player;
    float playerY;
    private bool scoring = true;

    void Start()
    {
        currentScoreText = GameObject.Find("CurrentScoreText").GetComponent<TextMeshProUGUI>();
        coinScoreText = GameObject.Find("CoinScoreText").GetComponent<TextMeshProUGUI>();

        scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();

        currentScoreText.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
        coinScoreText.text = PlayerPrefs.GetInt("MoneyScore",0).ToString();

        player = GameObject.FindGameObjectWithTag("Player");
        playerY = player.transform.position.y;
    }

    public void UpdateScore()
    {
        if(scoring)
        {
            scoreText.text = "Score:";
            currentScore++;
            currentScoreText.text = ""+currentScore;
            

            if(currentScore>PlayerPrefs.GetInt("HighScore", 0))
            {
                PlayerPrefs.SetInt("HighScore", currentScore);
                highScoreText.text = ""+PlayerPrefs.GetInt("HighScore", 0).ToString();
                FindObjectOfType<GameSceneController>().isHighScore = true;
            }
            if(currentScore>10)
            {
                yourScoreText.text = ""+currentScore;
                FindObjectOfType<GameSceneController>().isYourScore = true;
            }
            else
            {
                FindObjectOfType<GameSceneController>().isHighScore = false;
            }
        }
    }

    public void UpdateMoney()
    {
        moneyScore++;
        coinScoreText.text=""+moneyScore;
        int money = PlayerPrefs.GetInt("MoneyScore");
        PlayerPrefs.SetInt("MoneyScore",money+1);
        
    }

    public void Update()
    {
        if(playerY != player.transform.position.y)
        {
            coinScoreText.text=""+moneyScore;
            currentScoreText.text = ""+currentScore;
            playerY = player.transform.position.y;
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
