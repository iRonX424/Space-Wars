using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfig> waveConfigs;
    [SerializeField] int startingWave = 0;
    [SerializeField] bool looping = false;

    IEnumerator Start()
    {
        do
        {
           yield return StartCoroutine(spawnAllWaves());
        } while (looping) ;
    }

    private IEnumerator spawnAllWaves()
    {
        for (int waveIndex = startingWave; waveIndex < waveConfigs.Count; waveIndex++)
        {
            var currentWave = waveConfigs[waveIndex];
            yield return StartCoroutine(SpawnEnemiesInWave(currentWave));
        }
    }

    private IEnumerator SpawnEnemiesInWave(WaveConfig wave)
    {
        for (int i = 0; i < wave.getNumberOfEnemies(); i++)
        {
            var enemy=Instantiate(
                wave.getEnemyPrefab(),
                wave.getWaypoints()[0].transform.position,
                Quaternion.identity);
            enemy.GetComponent<EnemyPathing>().setWave(wave);
            yield return new WaitForSeconds(wave.getTimeBeteenSpawns());
        }
    }
}
