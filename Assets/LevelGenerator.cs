using UnityEngine;
using System.Collections.Generic;

public class LevelGenerator : MonoBehaviour
{
    private const float PLAYER_DISTANCE_SPAWN_LEVEL_PART = 10f;

    [SerializeField] private Transform LevelPrefab_Start;
    [SerializeField] private List<Transform> levelPrefabList;
    [SerializeField] private Transform LevelPrefab1;
    [SerializeField] private Transform ParentGrid;
    [SerializeField] private PlayerController player;
    private Vector3 lastEndPositon;
    private void Awake()
    {
        
        lastEndPositon = LevelPrefab_Start.Find("EndPosition").position;
        int startingSpawnLevelParts = 2;
        for(int i = 0; i < startingSpawnLevelParts; ++i)
        {
            SpawnLevelPrefab();
        }
    }

    private void Update()
    {
        Debug.Log("DISTANCE " + Vector3.Distance(player.CurrentPosition(), lastEndPositon));
        if(Vector3.Distance(player.CurrentPosition(), lastEndPositon) < PLAYER_DISTANCE_SPAWN_LEVEL_PART)
        {
            //spawning another level prefab
            SpawnLevelPrefab();
        }
    }

    private void SpawnLevelPrefab()     
    {
        Transform drawedLevelPrefab = levelPrefabList[Random.Range(0, levelPrefabList.Count)];

        Transform lastLevelPrefabTransform = SpawnLevelPrefab(drawedLevelPrefab, lastEndPositon);
        lastEndPositon = lastLevelPrefabTransform.Find("EndPosition").position;
        Debug.Log("lastEndPosition: " + lastEndPositon.ToString());
    }
    private Transform SpawnLevelPrefab(Transform levelPrefab, Vector3 spawnPosition)
    {
        Transform levelPrefabTransform = Instantiate(levelPrefab, spawnPosition, Quaternion.identity);
        levelPrefabTransform.transform.parent = ParentGrid;
        return levelPrefabTransform;
    }
}
