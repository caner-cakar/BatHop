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
        if(playedGameCount >=2f && score.currentScore >=5f && bats != null)
        {
            BeInvisible();
            PlayerPrefs.SetInt("playedGameCount",0);
        }
    }

    public void CountGame()
    {
        playedGameCount++;
        PlayerPrefs.SetInt("playedGameCount",playedGameCount);
        
    }

    private void BeInvisible()
    {
        if(bats !=null)
            bats.SetActive(true);
    }
}
