using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invisibility : MonoBehaviour
{
    private int playedGameCount = 0;
    private SpriteRenderer spriteRenderer;
    private Score score;
    void Start()
    {
        playedGameCount = PlayerPrefs.GetInt("playedGameCount");
        spriteRenderer = GetComponent<SpriteRenderer>();
        score = FindObjectOfType<Score>();
        CountGame();
        
    }

    void Update()
    {
        if(playedGameCount == 5)
            BeInvisible();
    }

    public void CountGame()
    {
        playedGameCount++;
        PlayerPrefs.SetInt("playedGameCount",playedGameCount);
        if(playedGameCount == 5)
        {
            PlayerPrefs.SetInt("playedGameCount",0);
        }
    }

    public void BeInvisible()
    {
        if(score.currentScore == 5)
        {
            spriteRenderer.enabled = false;
        }
        if(score.currentScore == 10)
        {
            spriteRenderer.enabled = true;
        } 
    }
}
