using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField] Text scoreText;
    int score = 0;

    [SerializeField] Text starScoreText;
    int starScore = 0;

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = ""+score;
        starScoreText.text = ""+starScore;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateScore()
    {
        score++;
        scoreText.text = ""+score;
    }

    public void UpdateStarScore()
    {
        starScore++;
        starScoreText.text = ""+starScore;
    }
}
