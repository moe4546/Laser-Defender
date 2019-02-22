using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Enemy Wave Config")]
public class WaveConfig : ScriptableObject
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject pathPrefab;
    [SerializeField] float timeBetweenSpawns = 0.5f;
    [SerializeField] float spawnRandomFactor = 0.3f;
    [SerializeField] int numberOfEnemies = 5;
    [SerializeField] float moveSpeed = 2f;

    public float GetMoveSpeed
    {
        get
        {
            return moveSpeed;
        }
    }

    public GameObject GetEnemyPrefab
    {
        get
        {
            return enemyPrefab;
        }
    }

    public List<Transform> GetWayPoins()
    {
        var waypoints = new List<Transform>();
        foreach(Transform child in pathPrefab.transform)
        {
            waypoints.Add(child);
        }
        return waypoints;
    }

    public float GetTimeBetweenSpawns
    {
        get
        {
            return timeBetweenSpawns;
        }
    }

    public float GetSpawnRandomFactor
    {
        get
        {
            return spawnRandomFactor;
        }
    }

    public int GetNumberOfEnemies
    {
        get
        {
            return numberOfEnemies;
        }
    }


}
