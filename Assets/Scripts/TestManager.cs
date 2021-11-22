using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestManager : MonoBehaviour
{
    public int sceneIndex;
    public GameObject[] scenes;
    public GameObject[] players;
    public GameObject[] waves;
    public GameObject beginningBruce;

    void Start()
    {
        if(sceneIndex > 5)
        {
            GameManager.HasErickPower = true;
        }
        if(sceneIndex > 2)
        {
            beginningBruce.SetActive(false);
        }
        if(sceneIndex > 6)
        {
            GameManager.HasGaryPower = true;
        }
        if(sceneIndex > 7)
        {
            GameManager.IsDoubleControlEnabled = true;
        }
        // cameraFreeLook.Follow = gameObject.transform;
        for(int i = 0; i < sceneIndex; i++)
        {
            // print("Deactivate" + i);
            scenes[i].SetActive(false);
            GameManager.LoadAndDestroyNextSceneTrigger();
        }

        GameObject currentScene = scenes[sceneIndex];
        currentScene.SetActive(true);
        int j = 0;
        foreach(GameObject player in players)
        {
            player.transform.position = currentScene.transform.position + new Vector3(0.0f, 0.0f, j * 5.0f);
            j++;
        }
    }
}
