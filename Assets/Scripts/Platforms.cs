using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Platforms : MonoBehaviour
{
    [SerializeField] GameObject appear;
    [SerializeField] private float interval  = 1f;
    public void Start()
    {
        //StartCoroutine(Appear());
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

    IEnumerator Appear()
    {
         while (true)
        {
            yield return new WaitForSeconds(interval);
            appear.SetActive(false);
            yield return new WaitForSeconds(interval);
            appear.SetActive(true);
                
            
        }
    }

}
