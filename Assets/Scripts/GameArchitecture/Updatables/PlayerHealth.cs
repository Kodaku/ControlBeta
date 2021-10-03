﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHealth : MonoBehaviour, Updatable
{
    [SerializeField] private Image healthImage;
    [SerializeField] private float health;
    private float maxHealth;

    public void AddQuantity(int quantity)
    {
        health += quantity;
        // print(health);
        if(health <= 0.0f)
        {
            health = 0.0f;
        }
        float currentHealth = health / maxHealth;
        healthImage.fillAmount = currentHealth;
    }

    public float GetQuantity()
    {
        return health;
    }

    public void UpdateHealth(float damageAmount)
    {
        health -= damageAmount;
        // print(health);
        if(health <= 0.0f)
        {
            health = 0.0f;
        }
        float currentHealth = health / maxHealth;
        healthImage.fillAmount = currentHealth;
    }
    // Start is called before the first frame update
    void Start()
    {
        maxHealth = health;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
