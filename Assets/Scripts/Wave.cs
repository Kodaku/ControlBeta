using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave
{
    public GameObject[] enemies;
    private Queue<GameObject> enemiesQueue = new Queue<GameObject>();
    private List<string> spawnedEnemies = new List<string>();

    public void LoadEnemies()
    {
        foreach(GameObject enemy in enemies)
        {
            // Debug.Log("Loading Enemy " + enemy.gameObject.name);
            enemy.SetActive(false);
            enemiesQueue.Enqueue(enemy);
        }
    }

    public void Spawn()
    {
        GameObject enemy = enemiesQueue.Dequeue();
        enemy.SetActive(true);
        enemy.GetComponent<AIBehaviourTree>().ResetTarget();
        SpecialAttackTargetManager.UpdateTargetNames(enemy.name);
        // Debug.Log("Add: " + enemy.name);
        spawnedEnemies.Add(enemy.name);
        // Debug.Log("Count: " + spawnedEnemies.Count);
    }

    public void RemoveEnemy(string enemy)
    {
        // Debug.Log("Spawned Enemies: " + spawnedEnemies.Count);
        // Debug.Log("Remove: " + enemy);
        spawnedEnemies.Remove(enemy);
    }

    public bool AreEnemiesFinished()
    {
        // Debug.Log(spawnedEnemies.Count);
        return spawnedEnemies.Count == 0;
    }

    public bool HasMoreEnemies()
    {
        return enemiesQueue.Count > 0;
    }
}
