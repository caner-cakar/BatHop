using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    Text currentScoreText;
    Text moneyScoreText;
    public int moneyScore = 0;
    public int currentScore = 0;

    [SerializeField] Text highScoreText;
    GameObject player;
    float playerY;
    private bool scoring = true;

    void Start()
    {
        currentScoreText = GameObject.Find("CurrentScoreText").GetComponent<Text>();
        moneyScoreText = GameObject.Find("MoneyScoreText").GetComponent<Text>();

        currentScoreText.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
        moneyScoreText.text = PlayerPrefs.GetInt("MoneyScore",0).ToString();

        player = GameObject.FindGameObjectWithTag("Player");
        playerY = player.transform.position.y;
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
                highScoreText.text = "New High Score: "+PlayerPrefs.GetInt("HighScore", 0).ToString();
                FindObjectOfType<GameSceneController>().isHighScore = true;
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
        moneyScoreText.text=""+moneyScore;
        int money = PlayerPrefs.GetInt("MoneyScore");
        PlayerPrefs.SetInt("MoneyScore",money+1);
        
    }

    public void Update()
    {
        if(playerY != player.transform.position.y)
        {
            moneyScoreText.text=""+moneyScore;
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
