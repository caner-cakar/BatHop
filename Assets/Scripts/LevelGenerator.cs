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
    private Vector3 lastEndPosition;

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
        
        Transform choosenLevelPart = levelPatternList[Random.Range(0,levelPatternList.Count)];
        while(choosenLevelPart== beforeLevelPart)
        {
            choosenLevelPart = levelPatternList[Random.Range(0,levelPatternList.Count)];
        }
        Transform lastLevelPartTransform = SpawnLevelPart(choosenLevelPart,lastEndPosition); 
        beforeLevelPart = choosenLevelPart;
        lastEndPosition = lastLevelPartTransform.Find("EndPosition").position;
        spawnedLevelParts.Add(lastLevelPartTransform);
    }
    private Transform SpawnLevelPart(Transform levelPart,Vector3 spawnPosition)
    {
        Transform levelPartTransform = Instantiate(levelPart,spawnPosition,Quaternion.identity);
        return levelPartTransform;
    }

    
}
