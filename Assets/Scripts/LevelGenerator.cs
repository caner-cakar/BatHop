using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    private const float playerDistanceSpawnLevelPart = 20f;
    private const float playerDistanceDestroyLevelPart = 20f;
    [SerializeField] private Transform startLevel; 
    [SerializeField] private List<Transform> levelPatternList; 
    private GameObject player;

    private List<Transform> spawnedLevelParts = new List<Transform>();

    private Transform beforeLevelPart = null;
    private Transform beforeBeforeLevelPart = null;
    private Vector3 lastEndPosition;

    int levelCounter=0;

    private void Awake()
    {
        lastEndPosition = startLevel.Find("EndPosition").position;
        for (int i = 0; i < 2; i++)
        {
            SpawnLevelPart();
        }
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update() 
    {
        float characterPosY = player.transform.position.y;
        float lastEndPositionY = lastEndPosition.y;
        float distanceToTarget = Mathf.Abs(characterPosY - lastEndPositionY);
        if(distanceToTarget < playerDistanceSpawnLevelPart)
        {
            SpawnLevelPart();
        }

        for (int i = 0; i < spawnedLevelParts.Count; i++)
        {
            Transform levelPart = spawnedLevelParts[i];
            if (levelPart != null)
            {
                float distanceToPlayer = Mathf.Abs(levelPart.position.y - characterPosY);
                if (characterPosY > levelPart.position.y + playerDistanceDestroyLevelPart)
                {
                    Destroy(levelPart.gameObject);
                    spawnedLevelParts.RemoveAt(i);
                    i--;
                }
            }
            else
            {
                spawnedLevelParts.RemoveAt(i);
                i--;
            }
        }
    }

    private void SpawnLevelPart()
    {
        Transform choosenLevelPart = GetRandomLevelPart();
        Transform lastLevelPartTransform = SpawnLevelPart(choosenLevelPart, lastEndPosition); 
        beforeLevelPart = choosenLevelPart;
        lastEndPosition = lastLevelPartTransform.Find("EndPosition").position;
        spawnedLevelParts.Add(lastLevelPartTransform);
    }

    private Transform GetRandomLevelPart()
    {
        List<Transform> availableLevelParts = new List<Transform>(levelPatternList);
        // Remove the last two chosen level parts from the available list
        if (beforeLevelPart != null)
        {
            availableLevelParts.Remove(beforeLevelPart);
            if (beforeBeforeLevelPart != null)
            {
                availableLevelParts.Remove(beforeBeforeLevelPart);
            }
        }

        beforeBeforeLevelPart = beforeLevelPart;


        Transform choosenLevelPart = availableLevelParts[Random.Range(0, availableLevelParts.Count)];
        return choosenLevelPart;
    }


    private Transform SpawnLevelPart(Transform levelPart, Vector3 spawnPosition)
    {
        Transform levelPartTransform = Instantiate(levelPart, spawnPosition, Quaternion.identity);
        return levelPartTransform;
    }
}
