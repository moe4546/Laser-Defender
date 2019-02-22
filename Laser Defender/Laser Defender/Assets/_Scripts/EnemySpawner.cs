using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] List<WaveConfig> waveConfigs;
    [SerializeField] bool looping = false;


    // Start is called before the first frame update
    IEnumerator Start()
    {
        do
        {
            yield return StartCoroutine(SpawnAllWaves());
        }
        while (looping);

    }

    private IEnumerator SpawnAllWaves()
    {
        for (int i = 0; i < waveConfigs.Count; i++)
        {
            yield return StartCoroutine(SpawnAllEnemyiesInWave(waveConfigs[i]));
        }
    }

    private IEnumerator SpawnAllEnemyiesInWave(WaveConfig waveConfig)
    {

        for (int i = 0; i < waveConfig.GetNumberOfEnemies; i++)
        {
            var newEnemy = Instantiate(
                waveConfig.GetEnemyPrefab,
                waveConfig.GetWayPoins()[0].position,
                Quaternion.identity);

            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig);

            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawns);
        }


    }
}
