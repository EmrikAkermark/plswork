using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveHandler : MonoBehaviour
{
    public List<EnemyWave> Waves = new List<EnemyWave>();

    private int nextWave;
    private float nextWaveSpawnTime;
    private float timer;
    private bool spawnStuff = true;
    void Start()
    {
        nextWaveSpawnTime = Waves[0].SpawnActivationTime;
    }

    void Update()
    {
        if (!spawnStuff)
            return;
        timer += Time.deltaTime;
        if(timer > nextWaveSpawnTime)
        {
            Waves[nextWave].Run();
            nextWave++;
            if(nextWave < Waves.Count)
            {
                nextWaveSpawnTime = Waves[nextWave].SpawnActivationTime;
            }
            else
            {
                spawnStuff = false;
            }
        }
    }
}
