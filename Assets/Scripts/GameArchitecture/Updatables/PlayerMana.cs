using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMana : MonoBehaviour, Updatable
{
    [SerializeField] private Image manaImage;
    [SerializeField] private float mana;
    [SerializeField] private float maxMana;

    public void AddQuantity(int quantity)
    {
        mana += (float)quantity;
        // print("Mana: " + mana);
        if(mana <= 0.0f)
        {
            mana = 0.0f;
        }
        if(mana >= 1000.0f)
        {
            mana = 1000.0f;
        }
        DisplayMana();
    }

    public float GetQuantity()
    {
        return mana;
    }

    public void DecreaseMana(float damageAmount)
    {
        mana -= damageAmount;
        // print("Mana: " + mana);
        if(mana <= 0.0f)
        {
            mana = 0.0f;
        }
        DisplayMana();
    }

    public void IncreaseMana()
    {
        mana += 0.05f;
        if(mana >= maxMana)
        {
            mana = maxMana;
        }
        DisplayMana();
    }

    private void DisplayMana()
    {
        float currentMana = mana / maxMana;
        manaImage.fillAmount = currentMana;
    }
    // Start is called before the first frame update
    void Start()
    {
        // maxMana = 1000.0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        DisplayMana();
    }
}
