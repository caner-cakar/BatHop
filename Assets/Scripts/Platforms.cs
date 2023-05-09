using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Platforms : MonoBehaviour
{
    public void Start()
    {
    }

    void Update()
    {

    }
    public void afterDeath()
    {

        SceneManager.LoadScene("GameScene");
    }

    public void BrokenPlatform(float onStayTimer)
    {
        onStayTimer += Time.deltaTime;
        if(onStayTimer>1)
        {
            afterDeath();
        }
    }
}
