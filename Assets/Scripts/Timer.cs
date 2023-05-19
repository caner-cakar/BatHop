using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public Slider healthSlider;
    [SerializeField] Transform player;
    float playerY;
    const float gameTime = 45f;
    float time;

    void Awake()
    {
        healthSlider.maxValue = gameTime;
        healthSlider.value = gameTime;
        time = gameTime;
    }
    void Start()
    {
        playerY = player.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if(playerY != player.transform.position.y)
        {
            time -= Time.deltaTime;

            if (time <= 0f)
            {
                StartCoroutine(RestartGame());
            }
            if(time>=45f)
                time = 45f;

            healthSlider.value = time;
        }
    }

    IEnumerator RestartGame()
    {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(0.5f);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }

    public void ExtraTime(int extra)
    {
        time = time + extra;
    }
}
