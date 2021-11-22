using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSensor : MonoBehaviour, Perceivable<float>
{
    public bool isBoss;
    private PlayerHealth healthComponent;
    private AIHealthBar aIHealthBar;

    void Start()
    {
        if(isBoss)
        {
            healthComponent = GetComponentInParent<PlayerHealth>();
        }
        else
        {
            aIHealthBar = GetComponentInParent<AIHealthBar>();
        }
    }
    public float GetMeasure()
    {
        float health = 0.0f;
        if(isBoss)
            health = healthComponent.GetQuantity();
        else
            health = aIHealthBar.GetQuantity();
        return health;
    }

    public void ResetTarget()
    {
        
    }
}
