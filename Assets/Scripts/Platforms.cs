using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Platforms : MonoBehaviour
{
    [SerializeField]private GameObject ground;
    [SerializeField]private GameObject normalPlatform;
    [SerializeField]private GameObject brokenPlatform;
    [SerializeField]private GameObject appearingPlatform;
    [SerializeField]private GameObject cloudPlatform;
    [SerializeField]private GameObject deathPlatform;

    float lastPositionX;
    float lastPositionY;
    float distanceX = 0.65f;
    float distanceY = -2f;


    [SerializeField]private float interval =1f;
    void Start()
    {
        lastPositionY = ground.transform.position.y;
        lastPositionX = ground.transform.position.x;
        SpawnFirstLevel();
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

    private void SpawnFirstLevel()
    {
        for (int i = 0; i < 5; i++)
        {
            float xPosition = lastPositionX + distanceX;
            //float yPosition = lastPositionY + distanceY;
            Instantiate(normalPlatform,new Vector3(xPosition,distanceY,0f),Quaternion.identity);
            Instantiate(normalPlatform,new Vector3(-xPosition,distanceY,0f),Quaternion.identity);
            lastPositionX = xPosition + distanceX;
        }
        
    }
}
