using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    private const float playerDistanceSpawnLevelPart = 100f;
    [SerializeField] private Transform startLevel; 
    [SerializeField] private Transform levelPattern1; 
    [SerializeField] private GameObject player;

    private Vector3 lastEndPosition;

    private void Awake()
    {
        lastEndPosition = startLevel.Find("EndPosition").position;
        for (int i = 0; i < 1; i++)
        {
            SpawnLevelPart();
        }
    }
    private void Update() 
    {
        if(Vector3.Distance(player.transform.position, lastEndPosition) < playerDistanceSpawnLevelPart)
        {
            SpawnLevelPart();
        }
    }
    private void SpawnLevelPart()
    {
        Transform lastLevelPartTransform = SpawnLevelPart(lastEndPosition);
        lastEndPosition = lastLevelPartTransform.Find("EndPosition").position;
    }
    private Transform SpawnLevelPart(Vector3 spawnPosition)
    {
        Transform levelPartTransform = Instantiate(levelPattern1,spawnPosition,Quaternion.identity);
        return levelPartTransform;
    }
}
