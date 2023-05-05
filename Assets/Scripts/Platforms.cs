using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Platforms : MonoBehaviour
{
    [SerializeField]private GameObject appearingPlatform;
    [SerializeField]private float interval =1f;
    void Start()
    {
        StartCoroutine(AppearPlatform());
    }

    void Update()
    {
        
    }
    IEnumerator AppearPlatform()
    {
        while (true)
        {
            yield return new WaitForSeconds(interval); // GameObject'i inaktif hale getirmek için belirtilen süre beklenir
            appearingPlatform.SetActive(false);
            yield return new WaitForSeconds(interval); // GameObject'i tekrar aktif hale getirmek için belirtilen süre beklenir
            appearingPlatform.SetActive(true);
        }
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
