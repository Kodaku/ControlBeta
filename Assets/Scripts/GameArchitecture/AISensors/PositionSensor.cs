using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionSensor : MonoBehaviour, Perceivable<Vector3>
{
    private GameObject target;
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }
    public Vector3 GetMeasure()
    {
        Vector3 playerPosition = target.transform.position;
        // Transform agentPosition = GameObject.FindGameObjectWithTag("Enemy").transform;
        return playerPosition;
    }

    public void ResetTarget()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }
}
