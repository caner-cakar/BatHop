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
        StartCoroutine(ShakeBats());
        if(scoreController.currentScore>visibleScore+20f)
        {
            BatsGetAway();
        }
    }

    private void MoveTowardsTarget()
    {
        Vector3 targetPosition = targetObject.transform.position;
        Vector3 direction = targetPosition - gameObject.transform.position;
        gameObject.transform.Translate(direction.normalized * 5f * Time.deltaTime);
        lastChildDistance = Vector3.Distance(lastChild.position,targetObject.transform.position);
        if(lastChildDistance<0.1f && !onPlayer )
        {
            visibleScore = scoreController.currentScore;
            onPlayer = true;
        }
    }

    IEnumerator ShakeBats()
    {
        foreach (Transform bat in bats)
        {
            float randomNumber = Random.Range(0f,0.05f);
            bat.localPosition += new Vector3(0f,randomNumber,0f);
            yield return new WaitForSeconds(0.01f);

            bat.localPosition -= new Vector3(0f,randomNumber,0f);
            yield return new WaitForSeconds(0.01f);
        }
    }

    private void BatsGetAway()
    {
        foreach (Transform bat in bats)
        {
            float randomNumber = Random.Range(0f,0.05f);
            bat.localPosition -= new Vector3(0f,randomNumber,0f);
        }
        Destroy(gameObject,2f);
    }
}
