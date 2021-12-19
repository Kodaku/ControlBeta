using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavesManagerController : MonoBehaviour
{
    public GameObject[] wavesManager;
    public Queue<GameObject> wavesManagerQueue = new Queue<GameObject>();
    private GameObject currentWaveManager;

    void Start()
    {
        foreach(GameObject waveManger in wavesManager)
        {
            wavesManagerQueue.Enqueue(waveManger);
            waveManger.SetActive(false);
        }
    }

    public void SpawnWaveManager()
    {
        currentWaveManager = wavesManagerQueue.Dequeue();
        currentWaveManager.SetActive(true);
    }

    public void SpawnEnemies()
    {
        currentWaveManager.GetComponent<WavesManager>().SpawnWave();
    }

    public void RemoveEnemy(string enemy)
    {
        currentWaveManager.GetComponent<WavesManager>().RemoveEnemy(enemy);
    }

    public bool AreEnemiesFinished()
    {
        return currentWaveManager.GetComponent<WavesManager>().AreEnemiesFinished();
    }


}
