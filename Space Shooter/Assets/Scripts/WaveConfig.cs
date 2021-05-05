using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Enemy Wave Config")]
public class WaveConfig : ScriptableObject
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject pathPrefab;
    [SerializeField] float timeBetweenSpawns =0.5f;
    [SerializeField] float spawnRandomFactor = 0.3f;
    [SerializeField] int numberOfEnemies = 5;
    [SerializeField] float moveSpeed = 2f;
    
    public GameObject getEnemyPrefab() { return enemyPrefab; }

    public List<Transform> getWaypoints() 
    {
        var waypoints = new List<Transform>();

        foreach(Transform points in pathPrefab.transform)
        {
            waypoints.Add(points);
        }

        return waypoints; 
    }

    public float getTimeBeteenSpawns() { return timeBetweenSpawns; }

    public float getSpawnRandomFactor() { return spawnRandomFactor; }

    public int getNumberOfEnemies() { return numberOfEnemies; }

    public float getMoveSpeed() { return moveSpeed; }
}
