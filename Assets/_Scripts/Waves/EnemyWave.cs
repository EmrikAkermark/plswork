using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWave : MonoBehaviour
{
    public float SpawnActivationTime;
    public int NumberOfSpawns;
    public float SpawnInterval;
    public GameObject Enemy;
    public List<Transform> SpawnPoints;
    public EnemyPath Path;

    private Vector3 adjustedWaypoint;
    private List<Vector3[]> availiablePaths = new List<Vector3[]>();

    public void Run()
    {
        for (int i = 0; i < SpawnPoints.Count; i++)
        {
            Vector3[] adjustedPath = new Vector3[Path.Waypoints.Count];
            for (int j = 0; j < Path.Waypoints.Count; j++)
            {
                adjustedWaypoint = Path.Waypoints[j] + SpawnPoints[i].position;
                adjustedWaypoint.z = 0;
                adjustedPath[j] = adjustedWaypoint;

            }
            availiablePaths.Add(adjustedPath);
        }
        StartCoroutine(SpawnWave());
    }
    
    void SpawnEnemy()
    {
        GameObject SpawnedEnemy = Instantiate(Enemy);
        EnemyMovement enMov = SpawnedEnemy.GetComponent<EnemyMovement>();
        
        enMov.SetupPath(availiablePaths[Random.Range(0, availiablePaths.Count)]);
    }

    IEnumerator SpawnWave()
    {
        float timer = 0, newSpawnTime = SpawnInterval;
        int spawnedEnemies = 0;
        bool isSpawning = true;
        while(isSpawning)
        {
            timer += Time.deltaTime;
            if(timer >= newSpawnTime)
            {
                SpawnEnemy();
                newSpawnTime += SpawnInterval;
                spawnedEnemies++;
                if(spawnedEnemies == NumberOfSpawns)
                {
                    isSpawning = false;
                }
            }
            yield return null;
        }

    }
}
