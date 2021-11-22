using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsTriggerManager : MonoBehaviour
{
    public GameObject[] sceneTriggersArray;
    private Queue<GameObject> sceneTriggers;
    // Start is called before the first frame update
    void Start()
    {
        sceneTriggers = new Queue<GameObject>();
        foreach(GameObject sceneTrigger in sceneTriggersArray)
        {
            sceneTrigger.SetActive(false);
            sceneTriggers.Enqueue(sceneTrigger);
        }
        LoadNextSceneTrigger();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadNextSceneTrigger()
    {
        if(sceneTriggers.Count > 0)
            sceneTriggers.Dequeue().SetActive(true);
    }

    public void LoadAndDestroyNextSceneTrigger()
    {
        if(sceneTriggers.Count > 0)
            sceneTriggers.Dequeue();
    }
}
