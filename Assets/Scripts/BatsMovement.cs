using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatsMovement : MonoBehaviour
{
    public Transform lastChild;
    public GameObject targetObject;
    private List<Transform> bats;
    Score scoreController;

    float lastChildDistance;
    float visibleScore =5f;
    bool onPlayer=false;
    public bool isPlayerDead = false;

    float targetSpeed = 10f;

    void Start()
    {
        scoreController = FindObjectOfType<Score>();
        targetObject = GameObject.FindGameObjectWithTag("Player");

        bats = new List<Transform>();
        foreach (Transform child in transform)
        {
            if (child.name.StartsWith("Bat"))
            {
                bats.Add(child);
            }
        }
    }

    void Update()
    {
        MoveTowardsTarget();
        //StartCoroutine(ShakeBats());
        if(scoreController.currentScore>visibleScore+150f && onPlayer)
        {
            transform.SetParent(null);
            BatsGetAway();
        }
    }

    private void MoveTowardsTarget()
    {
        if(isPlayerDead==false && !onPlayer)
        {
            Vector3 targetPosition = targetObject.transform.position;
            Vector3 direction = targetPosition - gameObject.transform.position;
            gameObject.transform.Translate(direction.normalized * targetSpeed *Time.deltaTime);
            lastChildDistance = Vector3.Distance(lastChild.position,targetObject.transform.position);
            
        }
        
        if(lastChildDistance<0.15f && !onPlayer )
        {
            visibleScore = scoreController.currentScore;
            onPlayer = true;
            targetSpeed = 5f;
            transform.SetParent(targetObject.transform);
        }
    }

    private void BatsGetAway()
    {
        foreach (Transform bat in bats)
        {
            float randomNumber = Random.Range(0f,5f);
            Vector3 targetPosition = targetObject.transform.position;
            Vector3 direction = targetPosition - new Vector3(10f,0f);
            bat.transform.Translate(-direction.normalized * randomNumber *Time.deltaTime);
        
        }
        isPlayerDead = true;
        Destroy(gameObject,5f);
    }
}
