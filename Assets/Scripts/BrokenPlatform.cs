using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenPlatform : MonoBehaviour
{
    public IEnumerator LetsBreak()
    {
        float elapsedTime = 0f;
        while (elapsedTime < 3f)
        {
            float randomNumber = Random.Range(0f,1.5f);
            Vector3 targetPosition = FindObjectOfType<PlayerMovement>().transform.position;
            Vector3 direction = targetPosition + new Vector3(0f,5f);
            transform.GetChild(0).Translate(-direction.normalized * randomNumber *Time.deltaTime);
            randomNumber = Random.Range(0f,2f);
            transform.GetChild(1).Translate(-direction.normalized * randomNumber *Time.deltaTime);
            randomNumber = Random.Range(0f,3f);
            transform.GetChild(2).Translate(-direction.normalized * randomNumber *Time.deltaTime);
            yield return null;
            elapsedTime += Time.deltaTime;
        }
        Destroy(gameObject);
    }

    void Start()
    {
        StartCoroutine(LetsBreak());
    }
}
