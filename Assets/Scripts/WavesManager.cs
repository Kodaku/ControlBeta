using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavesManager : MonoBehaviour
{
    public Wave[] waves;
    private Wave currentWave;
    private Wave previousWave;
    private int wavesIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        foreach(Wave wave in waves)
        {
            wave.LoadEnemies();
        }
        currentWave = waves[wavesIndex];
        previousWave = currentWave;
        // this.gameObject.SetActive(false);
    }

    public void SpawnWave()
    {
        // print("Spawn Wave");
        while(currentWave.HasMoreEnemies())
        {
            currentWave.Spawn();
            StartCoroutine(WaitBeforeSpawn());
        }
        if(wavesIndex < waves.Length - 1)
        {
            previousWave = currentWave;
            wavesIndex++;
            currentWave = waves[wavesIndex];
        }
    }

    public void RemoveEnemy(string enemy)
    {
        previousWave.RemoveEnemy(enemy);
    }

    public bool AreEnemiesFinished()
    {
        return previousWave.AreEnemiesFinished();
    }

    private IEnumerator WaitBeforeSpawn()
    {
        yield return new WaitForSeconds(2.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
