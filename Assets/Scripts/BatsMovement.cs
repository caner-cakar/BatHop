using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatsMovement : MonoBehaviour
{
    public Transform centerPoint;
    public Transform lastBat;
    
    public float moveSpeed = 5;
    private int visibleScore;
    private bool isVisible;

    private Score score;
    GameObject player;

    SpriteRenderer playerSprite;
    private void Start() 
    {
        isVisible = true;
        score = FindObjectOfType<Score>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerSprite = player.GetComponent<SpriteRenderer>();
    }
    
    private void Update()
    {
        if(score.currentScore >=5f &&  isVisible ==true)
            BeInvisible();
        if(score.currentScore >= visibleScore + 5 && isVisible == false)
        {
            BeVisible();
            gameObject.transform.SetParent(null);
        }
        if(player.GetComponent<PlayerMovement>().isDead)
            gameObject.transform.SetParent(null);
    }

    public void BeInvisible()
    {
        
        float lastChildDistance =0f;
        CenterPosition();
        foreach (Transform child in transform)
        {
            if (child.name.StartsWith("Bat"))
            {
                Vector3 positionA = child.transform.position;
                Vector3 positionB = centerPoint.transform.position;
                float distance  = Vector3.Distance(positionA,positionB);

                Vector3 direction = centerPoint.position - child.position;
                child.Translate(direction.normalized * moveSpeed * Time.deltaTime);

                if(distance < 0.2f)
                {
                    SpriteRenderer spriteRenderer = child.GetComponent<SpriteRenderer>();
                    spriteRenderer.enabled = false;
                }
                
                lastChildDistance =Vector3.Distance(lastBat.position,positionB);
            } 
        }
        if(lastChildDistance<0.1f)
        {
            playerSprite.enabled=false;
            visibleScore = score.currentScore;
            isVisible =false;
        }   
    }

    public void BeVisible()
    {
        float lastChildDistance =0f;
        foreach (Transform child in transform)
        {
            if (child.name.StartsWith("Bat"))
            {
                SpriteRenderer spriteRenderer = child.GetComponent<SpriteRenderer>();
                spriteRenderer.enabled = true;
                Vector3 targetPosition = new Vector3(Random.Range(-50, 50), Random.Range(-50, 50), child.position.z);

                Vector3 direction = targetPosition - child.position;
                child.Translate(direction.normalized *10f * Time.deltaTime);    
            }
        }
        playerSprite.enabled=true;   
        Vector3 positionB = centerPoint.transform.position;
        lastChildDistance =Vector3.Distance(lastBat.position,positionB);
        if(lastChildDistance>5f)
        {
            visibleScore = score.currentScore;
            isVisible =false;
        }   
        Destroy(gameObject,5f);
    }

    private void CenterPosition()
    {
        gameObject.transform.SetParent(GameObject.FindGameObjectWithTag("Player").transform);
        centerPoint = GameObject.FindGameObjectWithTag("Player").transform;
        centerPoint.position = GameObject.FindGameObjectWithTag("Player").transform.position; 
        gameObject.transform.position = GameObject.FindGameObjectWithTag("Player").transform.position; 
    }
}
