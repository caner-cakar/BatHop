using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invisibility : MonoBehaviour
{
    private int playedGameCount = 0;
    private Score score;

    public GameObject bats;
    void Start()
    {
        bats.SetActive(false);
        playedGameCount = PlayerPrefs.GetInt("playedGameCount");
        score = FindObjectOfType<Score>();
        CountGame();
        
    }

    void Update()
    {
        if(playedGameCount >=2f && score.currentScore >=5f && score.currentScore<10f)
            BeInvisible();
    }

    public void CountGame()
    {
        playedGameCount++;
        PlayerPrefs.SetInt("playedGameCount",playedGameCount);
        if(playedGameCount >=2f)
        {
            PlayerPrefs.SetInt("playedGameCount",0);
        }
    }

    private void BeInvisible()
    {
        bats.SetActive(true);
    }
}
