using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AIHealthBar :  MonoBehaviour, Updatable
{
    public Slider slider;
    public int maxHealth;
    public Gradient gradient;
    public Image fill;
    void Start()
    {
        slider.maxValue = maxHealth;
        slider.value = maxHealth;
        fill.color = gradient.Evaluate(1.0f);
    }
    public void AddQuantity(int quantity)
    {
        slider.value += quantity;

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    public float GetQuantity()
    {
        return slider.value;
    }
}
