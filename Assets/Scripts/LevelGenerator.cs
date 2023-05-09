using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    private const float playerDistanceSpawnLevelPart = 10f;
    [SerializeField] private Transform startLevel; 
    [SerializeField] private List<Transform> levelPatternList; 
    [SerializeField] private GameObject player;

    private Vector3 lastEndPosition;

    private void Awake()
    {
        lastEndPosition = startLevel.Find("EndPosition").position;
        for (int i = 0; i < 2; i++)
        {
            SpawnLevelPart();
        }
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
    }
    private void SpawnLevelPart()
    {
        Transform choosenLevelPart = levelPatternList[Random.Range(0,levelPatternList.Count)];
        Transform lastLevelPartTransform = SpawnLevelPart(choosenLevelPart,lastEndPosition);
        lastEndPosition = lastLevelPartTransform.Find("EndPosition").position;
    }
    private Transform SpawnLevelPart(Transform levelPart,Vector3 spawnPosition)
    {
        Transform levelPartTransform = Instantiate(levelPart,spawnPosition,Quaternion.identity);
        return levelPartTransform;
    }
}
