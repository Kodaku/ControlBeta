using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaSensor : MonoBehaviour, Perceivable<float>
{
    private PlayerMana manaComponent;
    void Start()
    {
        manaComponent = GetComponentInParent<PlayerMana>();
    }
    public float GetMeasure()
    {
        float mana = manaComponent.GetQuantity();
        return mana;
    }
}
