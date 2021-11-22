using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave
{
    public GameObject[] enemies;
    private Queue<GameObject> enemiesQueue = new Queue<GameObject>();

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
    }

    public bool HasMoreEnemies()
    {
        return enemiesQueue.Count > 0;
    }
}
