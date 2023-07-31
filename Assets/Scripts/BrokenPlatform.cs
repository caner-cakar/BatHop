using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenPlatform : MonoBehaviour
{
    public IEnumerator LetsBreak()
    {
        float elapsedTime = 0f;
        float randomNumberA = Random.Range(0.5f,2f);
        float randomNumberB = Random.Range(0.5f,2f);
        float randomNumberC = Random.Range(0.5f,2f);
        while (elapsedTime < 3f)
        {
            Vector3 targetPosition = FindObjectOfType<PlayerMovement>().transform.position;
            Vector3 direction = targetPosition + new Vector3(0f,5f);
            transform.GetChild(0).Translate(-direction.normalized * randomNumberA *Time.deltaTime);
            transform.GetChild(1).Translate(-direction.normalized * randomNumberB *Time.deltaTime);
            transform.GetChild(2).Translate(-direction.normalized * randomNumberC *Time.deltaTime);
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
