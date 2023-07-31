using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bats : MonoBehaviour
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
        if(playedGameCount >=5f && score.currentScore >=1000f && bats != null)
        {
            BeInvisible();
            PlayerPrefs.SetInt("playedGameCount",0);
        }
        if(playedGameCount<5f)
            Destroy(bats.gameObject);
        if(FindObjectOfType<PlayerMovement>().isDead == true)
        {
            Destroy(bats.gameObject);
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
