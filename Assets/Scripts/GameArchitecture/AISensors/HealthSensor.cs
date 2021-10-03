using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSensor : MonoBehaviour, Perceivable<float>
{
    private PlayerHealth healthComponent;

    void Start()
    {
        healthComponent = GetComponentInParent<PlayerHealth>();
    }
    public float GetMeasure()
    {
        float health = healthComponent.GetQuantity();
        return health;
    }
}
