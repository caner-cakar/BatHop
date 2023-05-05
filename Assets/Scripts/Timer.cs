using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public Slider healthSlider;
    const float gameTime = 45f;
    float time;

    void Awake()
    {
        healthSlider.maxValue = gameTime;
        healthSlider.value = gameTime;
        time = gameTime;
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;

        if (time <= 0)
        {
            StartCoroutine(RestartGame());
        }

        healthSlider.value = time;
    }

    IEnumerator RestartGame()
    {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(0.5f);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }
}
